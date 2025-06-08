namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    private int? _id;
    private string? _dni;
    private string? _nombre;
    private string? _apellido;
    private string? _email;
    private string? _contrasena;
    private string? _telefono;
    public int? Id
    {
        get => _id;
        set => _id = value;
    }
    public string? DNI
    {
        get => _dni;
        set => _dni = value;
    }

    public string? Nombre{
        get => _nombre;
        set => _nombre = value;
    }
    public string? Apellido{
        get => _apellido;
        set => _apellido = value;
    }
    public string? Email
    {
        get => _email;
        set => _email = value;
    }
public string? Contrasena
    { 
        get => _contrasena;
        set => _contrasena = value;
}
    public string? Telefono{
    get => _telefono;
    set => _telefono = value;
}


    public override string ToString()
    {
        return $"Persona: {Nombre} {Apellido}, DNI: {DNI}, Email: {Email}, Telefono: {Telefono}";
    }
}