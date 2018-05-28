using Harvest.Net.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Harvest.Net.Tests
{
    public class TaskFacts : FactBase, IDisposable
    {
        HarvestTask _todelete = null;

        [Fact]
        public async Task ListTasks_Returns()
        {
            var response = await Api.Tasks.ListAll();
            var list = response.Tasks;

            Assert.True(list != null, "Result list is null.");
            Assert.NotEqual(0, list.First().Id);
        }
        
        [Fact]
        public async Task Task_ReturnsTask()
        {
            var task = await Api.Tasks.Get(GetTestId(TestId.TaskId));

            Assert.NotNull(task);
            Assert.Equal("44harv", task.Name);
        }

        [Fact]
        public async Task DeleteTask_ReturnsTrue()
        {
            var task = await Api.Tasks.Add("Delete Task");

            await Api.Tasks.Delete(task.Id);
            var exception=await Assert.ThrowsAsync<Refit.ApiException>(async () =>
            {
                var taskCheck = await Api.Tasks.Get(task.Id);
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task CreateTask_ReturnsANewTask()
        {
            _todelete = await Api.Tasks.Add("Create Task");

            Assert.Equal("Create Task", _todelete.Name);
        }
        
        [Fact]
        public async Task UpdateTask_UpdatesOnlyChangedValues()
        {
            _todelete = await Api.Tasks.Add("Update Task");

            var updated = await Api.Tasks.Update(_todelete.Id, "Updated Task");
            
            // stuff changed
            Assert.NotEqual(_todelete.Name, updated.Name);
            Assert.Equal("Updated Task", updated.Name);
        }
        
        public void Dispose()
        {
            if (_todelete != null)
                Api.Tasks.Delete(_todelete.Id).Wait();
        }
    }
}
