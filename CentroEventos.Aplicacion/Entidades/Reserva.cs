using CentroEventos.Aplicacion.Enums;
namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    private int _id;
    private DateTime _fechaAltaReserva;
    private int _eventoId;
    private int _personaId;
    private EstadoAsistencia _estado;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    public DateTime FechaAltaReserva
    {
        get => _fechaAltaReserva;
        set => _fechaAltaReserva = value;
    }
    public int EventoId
    {
        get => _eventoId;
        set => _eventoId = value;
    }
    public int PersonaId
    {
        get => _personaId;
        set => _personaId = value;
    }
    public EstadoAsistencia Estado
    {
        get => _estado;
        set => _estado = value;
    }
    public override string ToString()
    {
        return $"Reserva: {Id}, Fecha de la reserva: {FechaAltaReserva} Evento ID: {EventoId}, Persona ID: {PersonaId}, Estado: {Estado}";
    }
}