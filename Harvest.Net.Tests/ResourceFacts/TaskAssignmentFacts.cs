using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Harvest.Net.Models;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class TaskAssignmentFacts:FactBase,IDisposable
    {
        TaskAssignment _todelete = null;
        HarvestTask _todelete1 = null;

        int _projectId = 15676716;

        [Fact]
        public async Task ListAssignments_Returns()
        {
            var response = await Api.TaskAssignments.ListAll(_projectId);
            var list = response.TaskAssignments;

            Assert.True(list != null, "Result list is null.");
            Assert.NotEqual(0, list.First().Id);
        }

        [Fact]
        public async Task Assignment_ReturnsAssignment()
        {
            var assignmentId = 184319877;

            var assignment = await Api.TaskAssignments.Get(_projectId, assignmentId);

            Assert.NotNull(assignment);
            Assert.Equal(assignmentId, assignment.Id);
        }

        [Fact]
        public async Task DeleteAssignment_ReturnsTrue()
        {
            _todelete1 = await Api.Tasks.Add("For_automated_tests");
            var assignment = await Api.TaskAssignments.Add(_projectId, _todelete1.Id);

            await Api.TaskAssignments.Delete(_projectId, assignment.Id);
            var exception = await Assert.ThrowsAsync<Refit.ApiException>(async () =>
            {
                var taskCheck = await Api.TaskAssignments.Get(_projectId, assignment.Id);
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task CreateAssignment_ReturnsANewAssignment()
        {
            var taskId = 9201520;
            _todelete = await Api.TaskAssignments.Add(_projectId, taskId);
            var id = _todelete.Id;
            Assert.NotNull(_todelete);
        }

        [Fact]
        public async Task UpdateTask_UpdatesOnlyChangedValues()
        {
            var taskId = 9201520;
            _todelete = await Api.TaskAssignments.Add(_projectId, taskId, hourlyRate: 1);

            var updated = await Api.TaskAssignments.Update(_projectId, _todelete.Id, hourlyRate: 2);

            // stuff changed
            Assert.NotEqual(_todelete.HourlyRate, updated.HourlyRate);
            Assert.Equal(2, updated.HourlyRate);
        }

        public void Dispose()
        {
            if (_todelete1 != null)
            {
                Api.Tasks.Delete(_todelete1.Id).Wait();
            }

            if (_todelete != null)
            {
                try
                {
                    Api.TaskAssignments.Delete(_projectId, _todelete.Id).Wait();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
