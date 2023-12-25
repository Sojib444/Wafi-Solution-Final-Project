using Kidoo.Learn.Enums;
using Kidoo.Learn.Questions;
using Kidoo.Learn.Questions.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Kidoo.Learn.Web.Pages.Questions;

public class CreateModal : PageModel
{
    [BindProperty]
    public CreateUpdateQuestionDto QuestionDto { get; set; }

    public IFormFile ImageFile { get; set; }

    public bool questionTypeBool { get; set; } 

    private readonly IQuestionAppService _questionAppService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public CreateModal(IQuestionAppService questionAppService, 
                       IWebHostEnvironment webHostEnvironment)
    {
        _questionAppService = questionAppService;
        _webHostEnvironment = webHostEnvironment;
    }

    public void OnGet()
    {
        QuestionDto = new CreateUpdateQuestionDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (QuestionDto.Subject == Subject.Story && QuestionDto.StoryGroup == null) throw new UserFriendlyException("Select a Story group.");
        if(QuestionDto.Subject != Subject.Story) QuestionDto.StoryGroup = null;

        if (QuestionDto.QuestionType == QuestionType.MultipleChoice)
        {
            // Condition for multiple choice.
            QuestionDto.Options[0].options = Options.OptionA;
            QuestionDto.Options[1].options = Options.OptionB;
            QuestionDto.Options[2].options = Options.OptionC;
            QuestionDto.Options[3].options = Options.OptionD;
        }
        else
        {
            QuestionDto.Options.Clear();
            QuestionDto.CorrectOptionId = null;
        }

        // Handle the image upload logic
        if (ImageFile != null && ImageFile.Length > 0) CreatedImageFile();

        // Call the service to create the question
        await _questionAppService.CreateAsync(QuestionDto);

        return RedirectToPage("/Questions/Index"); // Redirect to a success page or wherever you need
    } 

    private async void CreatedImageFile()
    {    
        // Save the file to wwwroot/images folder or any desired location
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", ImageFile.FileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await ImageFile.CopyToAsync(stream);
        }

        // Update the Question.ImagePath property with the image path
        QuestionDto.QuestionImageFile = $"/images/{ImageFile.FileName}";
    }
}
