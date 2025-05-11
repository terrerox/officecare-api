using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Repository;
using Services;
using Services.Equipments;

namespace Tests
{
    [TestFixture]
    public class EquipmentServiceTests
    {
        private Mock<IEquipmentRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private EquipmentService _service;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IEquipmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new EquipmentService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetMaintenancesByEquipmentIdAsync_ReturnsMappedList()
        {
            var maintenances = new List<MaintenanceTask> { new MaintenanceTask { Id = 1, Description = "desc" } };
            var dtos = new List<GetMaintenanceTaskDto> { new GetMaintenanceTaskDto { Id = 1, Description = "desc", Equipment = new GetEquipmentDto { Id = 1 } } };
            _repositoryMock.Setup(r => r.GetMaintenancesByEquipmentIdAsync(1)).ReturnsAsync(maintenances);
            _mapperMock.Setup(m => m.Map<List<GetMaintenanceTaskDto>>(maintenances)).Returns(dtos);

            var result = await _service.GetMaintenancesByEquipmentIdAsync(1);

            Assert.AreEqual(dtos, result);
        }

        [Test]
        public void GetMaintenancesByEquipmentIdAsync_ThrowsKeyNotFound_ReturnsGlobalExceptionHandler()
        {
            _repositoryMock.Setup(r => r.GetMaintenancesByEquipmentIdAsync(2)).ThrowsAsync(new KeyNotFoundException("not found"));
            Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.GetMaintenancesByEquipmentIdAsync(2));
        }
    }
} 