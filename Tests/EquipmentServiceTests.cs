using Xunit;
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
    public class EquipmentServiceTests
    {
        private Mock<IEquipmentRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private EquipmentService _service;

        public EquipmentServiceTests()
        {
            _repositoryMock = new Mock<IEquipmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new EquipmentService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetMaintenancesByEquipmentIdAsync_ReturnsMappedList()
        {
            var maintenances = new List<MaintenanceTask> { new MaintenanceTask { Id = 1, Description = "desc" } };
            var dtos = new List<GetMaintenanceTaskDto> { new GetMaintenanceTaskDto { Id = 1, Description = "desc", Equipment = new GetEquipmentDto { Id = 1 } } };
            _repositoryMock.Setup(r => r.GetMaintenancesByEquipmentIdAsync(1)).ReturnsAsync(maintenances);
            _mapperMock.Setup(m => m.Map<List<GetMaintenanceTaskDto>>(maintenances)).Returns(dtos);

            var result = await _service.GetMaintenancesByEquipmentIdAsync(1);

            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task GetMaintenancesByEquipmentIdAsync_ThrowsKeyNotFound_ReturnsGlobalExceptionHandler()
        {
            _repositoryMock.Setup(r => r.GetMaintenancesByEquipmentIdAsync(2)).ThrowsAsync(new KeyNotFoundException("not found"));
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.GetMaintenancesByEquipmentIdAsync(2));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedList()
        {
            var equipments = new List<Equipment> { new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) } };
            var dtos = new List<GetEquipmentDto> { new GetEquipmentDto { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) } };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(equipments);
            _mapperMock.Setup(m => m.Map<List<GetEquipmentDto>>(equipments)).Returns(dtos);

            var result = await _service.GetAllAsync();

            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task GetAllAsync_ThrowsException_ReturnsGlobalExceptionHandler()
        {
            _repositoryMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("fail"));
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto()
        {
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) };
            var dto = new GetEquipmentDto { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            _mapperMock.Setup(m => m.Map<GetEquipmentDto>(equipment)).Returns(dto);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_ThrowsGlobalExceptionHandler()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Equipment)null);
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.GetByIdAsync(2));
        }

        [Fact]
        public async Task CreateAsync_ReturnsMappedDto()
        {
            var upsertDto = new UpSertEquipmentDto { Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) };
            var dto = new GetEquipmentDto { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            _mapperMock.Setup(m => m.Map<Equipment>(upsertDto)).Returns(equipment);
            _repositoryMock.Setup(r => r.CreateAsync(equipment)).ReturnsAsync(equipment);
            _mapperMock.Setup(m => m.Map<GetEquipmentDto>(equipment)).Returns(dto);

            var result = await _service.CreateAsync(upsertDto);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_ReturnsGlobalExceptionHandler()
        {
            var upsertDto = new UpSertEquipmentDto { Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            _mapperMock.Setup(m => m.Map<Equipment>(upsertDto)).Throws(new Exception("fail"));
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.CreateAsync(upsertDto));
        }

        [Fact]
        public async Task UpdateAsync_ReturnsMappedDto()
        {
            var upsertDto = new UpSertEquipmentDto { Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) };
            var updated = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) };
            var dto = new GetEquipmentDto { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            _repositoryMock.Setup(r => r.UpdateAsync(equipment)).ReturnsAsync(updated);
            _mapperMock.Setup(m => m.Map(upsertDto, equipment));
            _mapperMock.Setup(m => m.Map<GetEquipmentDto>(updated)).Returns(dto);

            var result = await _service.UpdateAsync(1, upsertDto);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task UpdateAsync_NotFound_ThrowsGlobalExceptionHandler()
        {
            var upsertDto = new UpSertEquipmentDto { Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            _repositoryMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Equipment)null);
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.UpdateAsync(2, upsertDto));
        }

        [Fact]
        public async Task UpdateAsync_ThrowsException_ReturnsGlobalExceptionHandler()
        {
            var upsertDto = new UpSertEquipmentDto { Brand = "A", Model = "B", EquipmentTypeId = 1, PurchaseDate = new DateOnly(2020,1,1) };
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1) };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            _mapperMock.Setup(m => m.Map(upsertDto, equipment));
            _repositoryMock.Setup(r => r.UpdateAsync(equipment)).ThrowsAsync(new Exception("fail"));
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.UpdateAsync(1, upsertDto));
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue()
        {
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1), EquipmentMaintenances = new List<EquipmentMaintenance>() };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            _repositoryMock.Setup(r => r.DeleteAsync(equipment)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_NotFound_ThrowsGlobalExceptionHandler()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Equipment)null);
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.DeleteAsync(2));
        }

        [Fact]
        public async Task DeleteAsync_WithMaintenances_ThrowsGlobalExceptionHandler()
        {
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1), EquipmentMaintenances = new List<EquipmentMaintenance> { new EquipmentMaintenance() } };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.DeleteAsync(1));
        }

        [Fact]
        public async Task DeleteAsync_ThrowsException_ReturnsGlobalExceptionHandler()
        {
            var equipment = new Equipment { Id = 1, Brand = "A", Model = "B", EquipmentTypeId = 1, EquipmentType = new EquipmentType { Id = 1, Description = "Type1" }, PurchaseDate = new DateOnly(2020,1,1), EquipmentMaintenances = new List<EquipmentMaintenance>() };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(equipment);
            _repositoryMock.Setup(r => r.DeleteAsync(equipment)).ThrowsAsync(new Exception("fail"));
            await Assert.ThrowsAsync<GlobalExceptionHandler>(async () => await _service.DeleteAsync(1));
        }
    }
} 