namespace CentroEventos.Aplicacion.Excepciones;
public class ValidacionException : Exception
{
    public List<string> Errores { get; }

    public ValidacionException(string mensaje)
        : base(mensaje)
    {
        Errores = [mensaje];
    }

    public ValidacionException(List<string> errores)
        : base("Error de validación.")
    {
        Errores = errores;
    }
}