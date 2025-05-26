using Xunit;
using BugTracker.Models;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BugTracker.Tests
{
    public class PriorityLevelTests
    {
        [Fact]
        public void Bug_Should_HavePriorityLevel_Property()
        {
            // Arrange
            var bug = new Bug();
            
            // Act
            bug.Priority = PriorityLevel.High;
            
            // Assert
            Assert.Equal(PriorityLevel.High, bug.Priority);
        }
        
        [Fact]
        public void BugController_Create_Should_SetPriorityLevel()
        {
            // Arrange
            var controller = new BugController();
            var bug = new Bug
            {
                Title = "Test Bug",
                Description = "This is a test bug",
                Priority = PriorityLevel.Medium
            };
            
            // Act
            var result = controller.Create(bug) as RedirectToActionResult;
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            
            // Get the bugs list from the Index action to verify the bug was added with correct priority
            var indexResult = controller.Index() as ViewResult;
            Assert.NotNull(indexResult);
            var bugs = indexResult!.Model as IEnumerable<Bug>;
            Assert.NotNull(bugs);
            var addedBug = bugs!.FirstOrDefault(b => b.Title == "Test Bug");
            
            Assert.NotNull(addedBug);
            Assert.Equal(PriorityLevel.Medium, addedBug.Priority);
        }
    }
}
