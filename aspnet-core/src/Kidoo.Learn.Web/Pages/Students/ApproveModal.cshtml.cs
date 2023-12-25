using Kidoo.Learn.Enums;
using Kidoo.Learn.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Students;

public class ApproveModal : KidooPageModel
{
    [BindProperty(SupportsGet = true)]
    public StudentPaymentStatus PaymentStatus { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    private readonly IStudentAppService _studentAppService;

    public ApproveModal(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _studentAppService.Approve(Id, PaymentStatus);
        return Page();
    }
}
