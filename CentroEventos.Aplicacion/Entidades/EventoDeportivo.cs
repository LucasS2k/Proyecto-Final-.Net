namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    private int _id;
    private string? _nombre;
    private string? _descripcion;
    private DateTime? _fechaHoraInicio;
    private double _duracion;
    private int _cupo;
    // private enum _Estado (pendiente, en curso, finalizado)
    //id del responsable
    private int _responsableId;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    public string? Nombre
    {
        get => _nombre;
        set => _nombre = value;
    }

    public string? Descripcion
    {
        get => _descripcion;
        set => _descripcion = value;
    }
    public DateTime? FechaHoraInicio{
        get=> _fechaHoraInicio;
        set => _fechaHoraInicio = value;
    }
    public double Duracion
    {
        get => _duracion;
        set => _duracion = value;
    }
    public int Cupo
    {
        get => _cupo;
        set => _cupo = value;
    }
    public int ResponsableId
    {
        get => _responsableId;
        set => _responsableId = value;
    }
    public override string ToString()
    {
        return $"Evento Deportivo: {Nombre}, Descripcion: {Descripcion}, Fecha y Hora de Inicio: {FechaHoraInicio}, Duracion: {Duracion} horas, Cupo: {Cupo}, Responsable ID: {ResponsableId}";
    }   
}