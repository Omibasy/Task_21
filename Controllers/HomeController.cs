using Microsoft.AspNetCore.Mvc;
using Task_20.Model;
using Task_20.Model.Data;


namespace Task_20.Controllers
{

    public class HomeController : Controller
    {
       private IWebHostEnvironment _appEnvironment;
       private PhoneBook phoneBook;

        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            phoneBook = new PhoneBook();
    
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Person = phoneBook.GetPersonalities();
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
      

            ViewBag.Data = (await phoneBook.GetPersonalities(id),
                            await phoneBook.GetPersonalData(id),
                            $"/photo/photo_{id}.jpg");

            return View();
        }


        [HttpGet]
        public IActionResult DeleteView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UploadingNewData(Person person,
                                            PersonalData personalData ,IFormFile file)
        {
            try
            {
               phoneBook.AddingNewData(person, personalData, file, _appEnvironment);
              
               return Redirect("~/");
            }
            catch (Exception)
            {
                return BadRequest();
            } 
        }

        [HttpDelete]
        public IActionResult DeleteRecord(int id)
        {
            try
            {
                return Ok(phoneBook.DeleteAnEntry(id, _appEnvironment));
            }
            catch (Exception)
            {
                return NotFound();

            }
        }

 
        [HttpGet]
        public async Task<IActionResult> DataChangesView(int id) 
        {
            try
            {
                PersonalData personalData = await phoneBook.GetPersonalData(id);

                personalData.PhoneNumber = personalData.PhoneNumber.Replace("8", string.Empty);
                personalData.PhoneNumber = personalData.PhoneNumber.Replace("-", string.Empty);

                ViewBag.Data = (await phoneBook.GetPersonalities(id), personalData);

                return View();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        
        }

        [HttpGet]
        public IActionResult ChangePhotoView(int id) 
        {
            ViewBag.Data = (id, $"/photo/photo_{id}.jpg");


            return View();
        }

        [HttpPost]
        public  IActionResult ChangePhoto(int id,IFormFile file)
        {

           try
            {
               phoneBook.EditPhoto(id, file, _appEnvironment);

               return Redirect($"~/Home/Info?id={id}"); 
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }


        }
  
        [HttpPut]
        public IActionResult DataChangesView(Person person, PersonalData personalData) 
        {
            try
            {
                return Ok(phoneBook.DataChanges(person, personalData).Result);
            }
            catch (Exception e)
            {
               return BadRequest(e);
            }
        }
    }
}
