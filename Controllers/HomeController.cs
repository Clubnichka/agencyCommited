using agency.Data;
using agency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly agencyContext _context;

        public static int currentUserId;

        public static bool isAdmin;

        private object locker=new object();

        public HomeController(agencyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult chooser()
        {
            return View();
        }

        public IActionResult registration1()
        {
            return View();
        }

        public IActionResult registration2()
        {
            return View();
        }
        public IActionResult autorization()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create1(Employer usr)
        {
            usr.accessTime = 0;
            usr.companyName = "";
            usr.Description = "";
            _context.Add(usr);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/Index.cshtml");

        }


        [HttpPost]
        public async Task<IActionResult> Create2(Employee usr)
        {
            usr.aboutMe = "null";
            usr.expirience = 0;
            usr.age = 0;
            usr.requestedSalary = 0;
            usr.accessTime = 0;
            usr.education = "null";
            usr.reuestedPost = "null";
                _context.Add(usr);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/Index.cshtml");
            
        }

        [HttpPost]
        /* public async Task<IActionResult> Enter1(Employer usr)
         {
             List<Employer> list = _context.Employer.ToList();
             foreach (Employer curs in  list)
             {
                 if (curs.Name==usr.Name&&curs.Password==usr.Password)
                 {
                     currentUserId = usr.Id;
                     return View("~/Views/Home/HomeEmployer.cshtml",curs);
                 }
             }

             return View("~/Views/Home/Index.cshtml");

         }

         [HttpPost]
         public async Task<IActionResult> Enter2(Employee usr)
         {
             List<Employee> list = _context.Employee.ToList();
             foreach (Employee curs in list)
             {
                 if (curs.Name == usr.Name && curs.Password == usr.Password)
                 {
                     currentUserId = curs.Id;
                     await start(curs.Id);
                     return View("~/Views/Home/HomeEmployee.cshtml", curs);

                 }
             }

             return View("~/Views/Home/Index.cshtml");

         }*/
        public async Task<IActionResult> Enter(User usr)
        {
            List<Employer> list = _context.Employer.ToList();
            List<Employee> list1 = _context.Employee.ToList();
            List < Administrator > adm= _context.Administrator.ToList();
            List<Manager> manager= _context.Manager.ToList();
            foreach (Employer curs in list)
            {
                if (curs.Name == usr.Name && curs.Password == usr.Password)
                {
                    currentUserId = curs.Id;
                    if (curs.accessTime > 0) { return View("~/Views/Home/HomeEmployer.cshtml", curs); }
                    else { return View("~/Views/Home/Den1.cshtml", curs); }
                }
            }
            foreach (Employee curs in list1)
            {
                if (curs.Name == usr.Name && curs.Password == usr.Password)
                {
                    currentUserId = curs.Id;
                    if (curs.accessTime > 0) { return View("~/Views/Home/HomeEmployee.cshtml", curs); }
                    else { return View("~/Views/Home/Den2.cshtml", curs); }
                }
            }
            foreach (var curs in adm)
            {
                if (curs.Name == usr.Name && curs.Password == usr.Password)
                {
                    currentUserId = curs.Id;
                    isAdmin = true;
                    return View("~/Views/Home/HomeAdmin.cshtml", curs);
                }
            }
            foreach (var curs in manager)
            {
                if (curs.Name == usr.Name && curs.Password == usr.Password)
                {
                    currentUserId = curs.Id;
                    return View("~/Views/Home/HomeManager.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        public IActionResult pay(int id)
        {
                
                List<Employee>  list= _context.Employee.ToList();
                foreach (Employee curs in list)
                {
                    if (curs.Id == id)
                    {
                        return View(curs);
                    }
                }
                return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> payDay(int id)
        {
            List<Employee> list = _context.Employee.ToList();
            foreach (Employee curs in list)
            {
                if (curs.Id==id)
                {
                    curs.accessTime += 24;
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployee.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> payWeek(int id)
        {
            List<Employee> list = _context.Employee.ToList();
            foreach (Employee curs in list)
            {
                if (curs.Id == id)
                {
                    curs.accessTime += (24*7);
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployee.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> payMounth(int id)
        {
            List<Employee> list = _context.Employee.ToList();
            foreach (Employee curs in list)
            {
                if (curs.Id == id)
                {
                    curs.accessTime += (24*30);
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployee.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        public async Task<IActionResult> resume(int id)
        {
            var employee =await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            
            if (employee.accessTime > 0) 
            {
                return View(employee); 
            }
            else
            {
                return View("~/Views/Home/HomeEmployee.cshtml",employee);
            }
           
           
        }
        public async Task<IActionResult> FillRes(Employee resume)
        {
            _context.Update(resume);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/HomeEmployee.cshtml",resume);
        }

        public IActionResult pay2(int id)
        {

            List<Employer> list = _context.Employer.ToList();
            foreach (Employer curs in list)
            {
                if (curs.Id == id)
                {
                    return View(curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> payDay2(int id)
        {
            List<Employer> list = _context.Employer.ToList();
            foreach (Employer curs in list)
            {
                if (curs.Id == id)
                {
                    curs.accessTime += 24;
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployer.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> payWeek2(int id)
        {
            List<Employer> list = _context.Employer.ToList();
            foreach (Employer curs in list)
            {
                if (curs.Id == id)
                {
                    curs.accessTime += (24 * 7);
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployer.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> payMounth2(int id)
        {
            List<Employer> list = _context.Employer.ToList();
            foreach (Employer curs in list)
            {
                if (curs.Id == id)
                {
                    curs.accessTime += (24 * 30);
                    _context.Update(curs);
                    await _context.SaveChangesAsync();
                    return View("~/Views/Home/HomeEmployer.cshtml", curs);
                }
            }
            return View("~/Views/Home/Index.cshtml");

        }

        public IActionResult myVacList(int id)
        {
            List<Vacancy> Target=new List<Vacancy>();
            List<Vacancy> list = _context.Vacancy.ToList();
            foreach (Vacancy curs in list)
            {
                if (curs.CompanyId == id)
                {
                    Target.Add(curs);
                }
            }
            VacList listik=new VacList();
            listik.list = Target;
            listik.companyId = id;
            return View(listik);
        }

        public IActionResult backHome(VacList list)
        {
            int id = list.companyId;
            List<Employer> employers = _context.Employer.ToList();
            foreach(var employer in employers)
            {
                if (employer.Id == id)
                {
                    return View("~/Views/Home/HomeEmployer.cshtml",employer);
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> addvac(Vacancy vac)
        {
            List<Employer> list = _context.Employer.ToList();
            foreach (var employer in list)
            {
                if (employer.Id == vac.CompanyId)
                {
                    vac.CompanyId = employer.Id;
                    _context.Add(vac);
                    await _context.SaveChangesAsync();
                }
            }
            List<Vacancy> Target = new List<Vacancy>();
            List<Vacancy> list1 = _context.Vacancy.ToList();
            foreach (Vacancy curs in list1)
            {
                if (curs.CompanyId == vac.CompanyId)
                {
                    Target.Add(curs);
                }
            }
            VacList listik = new VacList();
            listik.list = Target;
            listik.companyId = vac.CompanyId;
            return View("~/Views/Home/myVacList.cshtml",listik);

        }
        public IActionResult createVac(VacList listik) 
        { 
            Vacancy vac = new Vacancy();
            vac.CompanyId = listik.companyId;
            return View(vac); 
        }

        public IActionResult viewvacancies(Employee employee)
        {
            List<Vacancy> list1 = _context.Vacancy.ToList();
            VacList listik = new VacList();
            listik.list = list1;
            listik.companyId = employee.Id;
            return View(listik);
        }

        public IActionResult backHome1(VacList list)
        {
            int id = list.companyId;
            List<Employee> employees = _context.Employee.ToList();
            foreach (var employee in employees)
            {
                if (employee.Id == id)
                {
                    return View("~/Views/Home/HomeEmployee.cshtml", employee);
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> dropVac(VacList listik)
        {
            List<Vacancy> list = _context.Vacancy.ToList();
            Vacancy target = new Vacancy();
            List<Vacancy> targetlist = new List<Vacancy>();
            foreach(var curs in list)
            {
                if(listik.curVacId == curs.Id)
                {
                    target = curs;
                }
            }
            _context.Vacancy.Remove(target);
            await _context.SaveChangesAsync();
            list = _context.Vacancy.ToList();
            foreach (var curs in list)
            {
                if (listik.companyId == curs.CompanyId)
                {
                    targetlist.Add(curs);
                }
            }
            listik.list=targetlist;
           return View("~/Views/Home/myVacList.cshtml", listik);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        
        static Timer t;
        static long interval = 3600000;
        static long interval2 = 30000;
        public async Task start(int id)
        {
                t = new Timer(new TimerCallback(a), null, 0, interval);
           
        }

        public async void a(object sender)
        {
            await substruct();
        }
        private async Task substruct()
        {
            var options = new DbContextOptionsBuilder<agencyContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=agency.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
            using (agencyContext context = new agencyContext(options.Options))
            {
                List<Employee> list = context.Employee.ToList();
                foreach (Employee curs in list)
                {
                    if (curs.Id == currentUserId)
                    {
                        curs.accessTime -= 1;
                        context.Update(curs);
                        await context.SaveChangesAsync();
                    }
            }
            }
            
                   
                
        } 
        public async Task<IActionResult> viewreqvacancies(int id)
        {
            List<Employee> emplist=_context.Employee.ToList();
            List <Vacancy> vlist= _context.Vacancy.ToList();
            var result = new VacList();
            result.companyId = id;
            result.list = new List<Vacancy>();
            var emp= new Employee();
            foreach (var cur in emplist)
            {
                if (cur.Id == id)
                {
                    emp = cur;
                }
            }
            foreach (var v in vlist)
            {
                if (v.salary >= emp.requestedSalary)
                {
                    if (v.requiredAge <= emp.age)
                    {
                        if (v.requiredExpirience <= emp.expirience)
                        {
                            if (v.Name == emp.reuestedPost)
                            {
                                result.list.Add(v);
                            }
                        }
                    }
                }
            }
            if (result.list!=null) {return View(result); }
            else
            {
                return View("~/Views/Home/NoFound.cshtml",result);
            }
        }
        public IActionResult viewcand(Employer employer)
        {
            List<Employee> list1 = _context.Employee.ToList();
            CandList listik = new CandList();
            listik.list = list1;
            listik.companyId = employer.Id;
            return View(listik);
        }
        public IActionResult viewreqcand(int id)
        {
            List<Employer> emplist = _context.Employer.ToList();
            List<Employee> clist = _context.Employee.ToList();
            List<Vacancy> list = _context.Vacancy.ToList();
            List<Vacancy> Target=new List<Vacancy>();
            foreach (Vacancy curs in list)
            {
                if (curs.CompanyId == id)
                {
                    Target.Add(curs);
                }
            }
            var result = new CandList();
            result.companyId = id;
            result.list = new List<Employee>();
            var emp = new Employer();
            foreach (var cur in emplist)
            {
                if (cur.Id == id)
                {
                    emp = cur;
                }
            }
            foreach (var c in clist)
            {
                foreach (var v in Target) {
                    if (c.requestedSalary <= v.salary)
                    {
                        if (v.requiredAge <= c.age)
                        {
                            if (v.requiredExpirience <= c.expirience)
                            {
                                if (v.Name == c.reuestedPost)
                                {
                                    result.list.Add(c);
                                }
                            }
                        }
                    } }
                
            }
            if (result.list != null) { return View(result); }
            else
            {
                return View("~/Views/Home/NoFound1.cshtml", result);
            }
        }
        public async Task<IActionResult> ad()
        {
            Administrator administrator = new Administrator();
            administrator.Name="admin";
            administrator.Email="proventusavenici@gmail.com";
            administrator.Password = "1893";
            _context.Add(administrator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult ADmanager()
        {
            var listik = _context.Manager.ToList();
            return View(listik);
        }

        public async Task<IActionResult> ADdropM(int mID)
        {
            List<Manager> list = _context.Manager.ToList();
            Manager target = new Manager();
            List<Manager> targetlist = new List<Manager>();
            foreach (var curs in list)
            {
                if (mID == curs.Id)
                {
                    target = curs;
                }
            }
            _context.Manager.Remove(target);
            await _context.SaveChangesAsync();
            list = _context.Manager.ToList();
            return View("~/Views/Home/ADmanager.cshtml", list);
        }
        public IActionResult ADaddM()
        {
            return View();
        }
        public async Task<IActionResult> addM(Manager m)
        {
            _context.Manager.Add(m);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/ADmanager.cshtml",_context.Manager.ToList());
        }

        public IActionResult ADEmployer()
        {
            var listik = _context.Employer.ToList();
            return View(listik);
        }

        public IActionResult ADEmployee()
        {
            var listik = _context.Employee.ToList();
            return View(listik);
        }

        public IActionResult ADvac()
        {
            var listik = _context.Vacancy.ToList();
            return View(listik);
        }

        public async Task<IActionResult> ADdropE1(int e1ID)
        {
            List<Employee> list = _context.Employee.ToList();
            Employee target = new Employee();
            List<Employee> targetlist = new List<Employee>();
            foreach (var curs in list)
            {
                if (e1ID == curs.Id)
                {
                    target = curs;
                }
            }
            _context.Employee.Remove(target);
            await _context.SaveChangesAsync();
            list = _context.Employee.ToList();
            return View("~/Views/Home/ADEmployee.cshtml",list);
        }

        public async Task<IActionResult> ADdropE2(int e2ID)
        {
            List<Employer> list = _context.Employer.ToList();
            Employer target = new Employer();
            List<Employer> targetlist = new List<Employer>();
            foreach (var curs in list)
            {
                if (e2ID == curs.Id)
                {
                    target = curs;
                }
            }
            _context.Employer.Remove(target);
            await _context.SaveChangesAsync();
            list = _context.Employer.ToList();
            return View("~/Views/Home/ADEmployer.cshtml", list);
        }

        public async Task<IActionResult> ADdropV(int vID)
        {
            List<Vacancy> list = _context.Vacancy.ToList();
            Vacancy target = new Vacancy();
            List<Vacancy> targetlist = new List<Vacancy>();
            foreach (var curs in list)
            {
                if (vID == curs.Id)
                {
                    target = curs;
                }
            }
            _context.Vacancy.Remove(target);
            await _context.SaveChangesAsync();
            list = _context.Vacancy.ToList();
            return View("~/Views/Home/ADvac.cshtml", list);
        }

        public async Task<IActionResult> addE1(Employee m)
        {
            m.aboutMe = "null";
            m.requestedSalary = 0;
            m.expirience = 0;
            m.accessTime = 0;
            m.reuestedPost = "null";
            m.age = 0;
            m.education = "null";
            _context.Employee.Add(m);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/ADEmployee.cshtml", _context.Employee.ToList());
        }

        public async Task<IActionResult> addE2(Employer m)
        {
            m.Description = "null";
            m.accessTime = 0;
            m.companyName = "null";
            _context.Employer.Add(m);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/ADEmployer.cshtml", _context.Employer.ToList());
        }

        public IActionResult ADaddE1()
        {
            return View();
        }

        public IActionResult ADaddE2()
        {
            return View();
        }

        public IActionResult HomeAdmin() {
            var adm = _context.Administrator.FirstOrDefault(m => m.Id ==currentUserId);
            if (isAdmin)
            {

                return View(adm);
            }
            else { return View(adm); }
                
        }

        public IActionResult cu() {
            return View(currentUserId);
        }
    }
}
       

