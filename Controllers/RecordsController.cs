using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendRecord03.Data;
using AttendRecord03.Models;
using Microsoft.AspNetCore.Authorization;

namespace AttendRecord03.Controllers
{
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Records
        public async Task<IActionResult> Index()
        {
            return View(await _context.Record.ToListAsync());
        }









        // GET: Records/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }




        //POST Records/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            int SearchPhraseInt = 0;

            try
            {
                SearchPhraseInt =  Int32.Parse(SearchPhrase);
            }
            catch
            {
                SearchPhraseInt = 99999;

            }
                

            return View("Index", 
                await _context.Record.
                Where( j => 
                j.PersonName.Contains(SearchPhrase)    || 
                j.PersonEmail.Contains(SearchPhrase)   ||
                j.AbsenceHours.Equals(SearchPhraseInt) ||
                j.AbsenceType.Contains(SearchPhrase)   ||
                j.Id.Equals(SearchPhraseInt)
                ).ToListAsync());
        }





        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record
                .FirstOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // GET: Records/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonEmail,PersonName,AbsenceType,AbsenceTimeStart,AbsenceTimeEnd,AbsenceHours")] Record record)
        {

            string userEmail = @User.Identity.Name;

            record.PersonEmail = userEmail;

            if (record.AbsenceTimeStart < record.AbsenceTimeEnd)
            {

                DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
                DateTime date2 = new DateTime(2009, 8, 2, 0, 0, 0);

                System.TimeSpan oneDayDiff = date2.Subtract(date1);

                int result = DateTime.Compare(date1, date2);

                date1 = record.AbsenceTimeStart;
                date2 = record.AbsenceTimeEnd;

                result = DateTime.Compare(date1, date2);

                System.TimeSpan diff = date2.Subtract(date1);

                if ( diff.Days > 0 )
                {

                    ViewBag.AbsDaysErr = "alert('Hi! Absence Time is greater than a day ');";
                    return View(record);
                }


                if (record.AbsenceHours == diff.Hours)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(record);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }



                if (record.AbsenceHours != diff.Hours)
                {
                    string diffStr = diff.Hours.ToString();

                    ViewBag.AbsHoursErr = "alert('Hi! Absence Hours should be " + diffStr + " hours ');";

                    if(diff.Hours  == 1 )
                    {
                        ViewBag.AbsHoursErr = "alert('Hi! Absence Hours should be " + diffStr + " hour ');";
                    }

                }

            }




            if (record.AbsenceTimeStart > record.AbsenceTimeEnd)
            {
                ViewBag.AbsTimeErr = "alert('Hi! Absence End Time is greater than Start Time, are you from the future ?!');";
            }




            return View(record);
        }

        // GET: Records/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonEmail,PersonName,AbsenceType,AbsenceTimeStart,AbsenceTimeEnd,AbsenceHours")] Record record)
        {
            if (id != record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(record);
        }

        // GET: Records/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record
                .FirstOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Records/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Record.FindAsync(id);
            _context.Record.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.Id == id);
        }
    }
}
