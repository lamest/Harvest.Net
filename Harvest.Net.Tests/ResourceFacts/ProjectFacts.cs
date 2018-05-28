using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harvest.Net.Models;
using Refit;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class ProjectFacts : FactBase, IDisposable
    {
        public void Dispose()
        {
            if (_todelete != null)
                Api.Projects.Delete(_todelete.Id).Wait();
        }

        private Project _todelete;

        [Fact]
        public async Task CreateProject_ReturnsANewProject()
        {
            _todelete = await Api.Projects.Add(6236766, "Test_create_project", false, "none", "none");

            Assert.Equal("Test_create_project", _todelete.Name);
        }

        [Fact]
        public async Task DeleteProjects_ReturnsTrue()
        {
            var project = await Api.Projects.Add(6236766, "Test_create_project", false, "none", "none");

            await Api.Projects.Delete(project.Id);
            var exception = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                var projectCheck = await Api.Projects.Get(project.Id);
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task ListProjects_Returns()
        {
            var response = await Api.Projects.ListAll();
            var list = response.Projects;

            Assert.True(list != null, "Result list is null.");
            Assert.NotEqual(0, list.First().Id);
        }

        [Fact]
        public async Task Project_ReturnsProject()
        {
            var project = await Api.Projects.Get(15676716);

            Assert.NotNull(project);
            Assert.Equal("TimeFlip", project.Name);
        }

        [Fact]
        public async Task UpdateTask_UpdatesOnlyChangedValues()
        {
            _todelete = await Api.Projects.Add(6236766, "Test_create_project", false, "none", "none");

            var updated = await Api.Projects.Update(_todelete.Id, name:"Updated Project");

            // stuff changed
            Assert.NotEqual(_todelete.Name, updated.Name);
            Assert.Equal("Updated Project", updated.Name);
        }
    }
}