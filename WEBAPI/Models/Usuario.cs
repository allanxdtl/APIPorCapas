using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int IdRol { get; set; }

    public virtual Role? IdRolNavigation { get; set; }
}
