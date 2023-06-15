using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCRUD.Data;
using MVCRUD.Models;

namespace MVCRUD.Controllers
{
    public class DeletedRecordController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public DeletedRecordController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        public IActionResult Index()
        {
            var deletedRecords = mvcDemoDbContext.Employees.Where(x => x.RecStatus == 'D').ToList();
            return View(deletedRecords);
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            var deletedRecord = mvcDemoDbContext.Employees.FirstOrDefault(x => x.Id == id);
            deletedRecord.RecStatus = 'A';
            mvcDemoDbContext.Employees.Update(deletedRecord);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = mvcDemoDbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                employee.RecStatus = 'D';

                // for deleting a record use the below code. 
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
