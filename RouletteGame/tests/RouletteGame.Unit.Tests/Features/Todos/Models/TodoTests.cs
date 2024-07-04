using FluentAssertions;
using RouletteGame.Features.Todos.Domain;
using RouletteGame.Features.Todos.Domain.Events;

namespace RouletteGame.Unit.Tests.Features.Todos.Models
{
    public class TodoTests
    {
        [Fact]
        public void Todo_Complete_ShouldUpdateCompleted()
        {
            // Arrange
            var item = new Todo
            {
                Id = Guid.NewGuid(),
                Text = "My todo item"
            };

            // Act
            item.Complete();

            // Assert
            item.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public void Todo_Complete_ShouldAddEvent()
        {
            // Arrange
            var item = new Todo
            {
                Id = Guid.NewGuid(),
                Text = "My todo item"
            };

            // Act
            item.Complete();

            // Assert
            item.StagedEvents.Should().ContainSingle(x => x is TodoCompletedEvent, "because the item was completed");
        }
    }
}