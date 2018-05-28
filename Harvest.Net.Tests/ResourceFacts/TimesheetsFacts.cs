using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harvest.Net.Models;
using Refit;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class TimesheetsFacts : FactBase, IDisposable
    {
        public void Dispose()
        {
            if (_todelete != null)
                Api.Timesheets.Delete(_todelete.Id).Wait();
        }

        private TimeEntry _todelete;

        [Fact]
        public async Task CreateTimeEntryViaDuration_ReturnsANewTimeEntry()
        {
            //TODO: read setting from settings api and create via duration or via start and end time
            _todelete = await Api.Timesheets.AddViaDuration(15676716, 9594774, DateTime.Today, hours: 5);

            Assert.NotNull(_todelete);
            Assert.Equal(5, _todelete.Hours);
        }

        [Fact]
        public async Task DeleteEntry_ReturnsTrue()
        {
            TimeEntry entry = null;

            var company = await Api.Companies.GetAsync();
            var wantsTimeStamps = company.WantsTimestampTimers;
            if (wantsTimeStamps)
            {
                entry = await Api.Timesheets.AddViaStartAndEndTime(15676716, 9594774, DateTime.Today.Date); //, startedTime: TimeSpan.FromMinutes(1), endedTime: TimeSpan.FromMinutes(2));
            }
            else
            {
                entry = await Api.Timesheets.AddViaDuration(15676716, 9594774, DateTime.Today);
            }

            await Api.Timesheets.Delete(entry.Id);
            var exception = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                var taskCheck = await Api.Timesheets.Get(entry.Id);
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task ListEntries_Returns()
        {
            var response = await Api.Timesheets.ListAll();
            var list = response.TimeEntries;

            Assert.True(list != null, "Result list is null.");
            Assert.NotEqual(0, list.First().Id);
        }

        [Fact]
        public async Task TimeEntry_ReturnsTimeEntry()
        {
            var entryId = GetTestId(TestId.TimesheetId);
            var timeEntry = await Api.Timesheets.Get(entryId);

            Assert.NotNull(timeEntry);
        }

        [Fact]
        public async Task UpdateTimeEntry_UpdatesOnlyChangedValues()
        {
            var initialHours = 5;
            var updatedHours = 6;
            _todelete = await Api.Timesheets.AddViaDuration(15676716, 9594774, DateTime.Today, hours: initialHours);

            var updated = await Api.Timesheets.Update(_todelete.Id, hours: updatedHours);

            // stuff changed
            Assert.NotEqual(_todelete.Hours, updated.Hours);
            Assert.Equal(updatedHours, updated.Hours);
        }
    }
}