using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCCF_MVC_App.DTOs;
using NCCF_MVC_App.Models;
using Newtonsoft.Json;

namespace NCCF_MVC_App.Controllers
{
    public class MembersController : Controller
    {
        private readonly NCCF_DatabaseContext _context;

        public MembersController(NCCF_DatabaseContext context){
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            // Not related to this code section, but it is presently up for keeps;
            //var UsersessionData = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("UserSession"));
            //if (UsersessionData == null)
            //{
            //    return Content("No session data found");

            //}
            var data = await _context.Members.Join(
                _context.Rooms,
                member => member.RoomId,
                room => room.RoomId,
                (member, room) => new
                {
                    member,
                    room,
                })
                .Join(
                _context.Units,
                m => m.member.UnitId,
                unit => unit.UnitId,
                (m, unit) => new
                {
                    m,
                    unit
                }).Select(s => new MemberDto
                {
                    MemberName = s.m.member.MemberName,
                    RoomName = s.m.room.RoomName,
                    Age = s.m.member.Age,
                    PostHeld = s.m.member.PostHeld,
                    UnitId = s.m.member.UnitId,
                    Name = s.unit.Name
                }).ToListAsync();

              return _context.Members != null ? 
                          View(data) :
                          Problem("Entity set 'NCCF_DatabaseContext.Members'  is null.");
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            var roomdata = _context.Rooms.Select(x => new
            {
                x.RoomId,
                x.RoomName
            }).ToList();
            var unitdata = _context.Units.Select(s => new
            {
                s.UnitId,
                s.Name
            }).ToList();
            SelectList roomsList = new SelectList(roomdata, "RoomId", "RoomName");
            SelectList unitsList = new SelectList(unitdata, "UnitId", "Name");
            ViewBag.RoomsList = roomsList;
            ViewBag.UnitsList = unitsList;
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MemberName,RoomId,Age,UnitId,PostHeld")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,MemberName,RoomId,Age,UnitId,PostHeld")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'NCCF_DatabaseContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            var userSession = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("UserSession"));
            userSession = null;
            return RedirectToAction("Login", "Home");
        }

        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
