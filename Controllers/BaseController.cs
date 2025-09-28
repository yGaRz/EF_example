﻿using EF_example.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly IValidationStorage _validationStorage;

    protected BaseController(IValidationStorage validationStorage)
    {
        _validationStorage = validationStorage;
    }

    protected async Task<IActionResult> HandleRequestAsync(Func<CancellationToken, Task<bool>> action, CancellationToken ct)
    {
        bool valid = await action(ct);
        if (!valid)
        {
            return BadRequest(_validationStorage.ToString());
        }
        return Ok();
    }

    protected async Task<IActionResult> HandleRequestAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken ct)
    {
        T result = await action(ct);
        if (!_validationStorage.IsValid)
        {
            return BadRequest(_validationStorage.ToString());
        }
        return Ok(result);
    }
}