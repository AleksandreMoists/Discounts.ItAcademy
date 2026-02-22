using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.MVC.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var categories = await _categoryService.GetAllAsync(ct);
        return View(categories);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _categoryService.CreateAsync(dto, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var category = await _categoryService.GetByIdAsync(id, ct);
        return View(new CategoryUpdateDto
        {
            Name = category.Name,
            Description = category.Description
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryUpdateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _categoryService.UpdateAsync(id, dto, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _categoryService.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
