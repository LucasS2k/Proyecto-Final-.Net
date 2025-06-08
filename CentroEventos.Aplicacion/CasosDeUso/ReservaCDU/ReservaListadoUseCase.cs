using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.CasosDeUso.ReservaCDU;
public class ReservaListadoUseCase(IRepositorioReserva repositorio)
{
    private readonly IRepositorioReserva _repositorio = repositorio;

    public List<CentroEventos.Aplicacion.Entidades.Reserva> Ejecutar()
    {
        return _repositorio.Listar();
    }
}