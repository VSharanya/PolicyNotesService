using PolicyNotesService.Models;
using PolicyNotesService.Services;
using PolicyNotesService.Repositories;
using Moq;

namespace PolicyNotesService.Tests.Unit;

public class PolicyNotesServiceUnitTests
{
    [Fact]
    public async Task AddNote_ShouldAddSuccessfully()
    {
        var mockRepo = new Mock<IPolicyNotesRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<PolicyNote>()))
                .ReturnsAsync((PolicyNote n) => n);

        var service = new PolicyNotesService.Services.PolicyNotesService(mockRepo.Object);

        var dto = new PolicyNoteCreateDto { PolicyNumber = "PN001", Note = "First note" };

        var result = await service.AddNoteAsync(dto);

        Assert.Equal("PN001", result.PolicyNumber);
    }

    [Fact]
    public async Task GetAllNotes_ShouldReturnList()
    {
        var mockRepo = new Mock<IPolicyNotesRepository>();
        mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<PolicyNote> {
                new PolicyNote{ Id=1, PolicyNumber="PN01", Note="Test" }
            });

        var service = new PolicyNotesService.Services.PolicyNotesService(mockRepo.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
    }
}
