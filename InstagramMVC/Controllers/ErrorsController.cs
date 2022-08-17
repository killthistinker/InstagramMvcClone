using System.Collections.Generic;
using InstagramMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace homework58.Controllers
{
    public class ErrorsController : Controller
    {
    private const string OppsMessage = "Oops... Ошибка";
    private readonly Dictionary<int, ErrorViewModel> _errorResolver;

    public ErrorsController()
    {
        _errorResolver = new Dictionary<int, ErrorViewModel>();
        _errorResolver.Add(404, new ErrorViewModel
        {
            StatusCode = 404,
            Message = "Ресурс не найден",
            Title = OppsMessage
        });
        _errorResolver.Add(403, new ErrorViewModel
        {
            StatusCode = 403,
            Message = "У вас нет прав доступа к данному ресурсу",
            Title = OppsMessage
        });
        _errorResolver.Add(500, new ErrorViewModel
        {
            StatusCode = 500,
            Message = "Сервер не смог обработать запрос",
            Title = OppsMessage
        });
    }

    [Route("Error/{statusCode}")]
    [ActionName("Error")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        if (_errorResolver.ContainsKey(statusCode))
        {
            return View(_errorResolver[statusCode]);
        }
        return View(_errorResolver[404]);
    }
}
}