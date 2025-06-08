using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Enums;
namespace CentroEventos.Aplicacion.CasosDeUso.ReservaCDU;
public class ReservaBajaUseCase(IRepositorioReserva repositorio, IServicioAutorizacion autorizacion)
{
    private readonly IRepositorioReserva _repositorio = repositorio;
    private readonly IServicioAutorizacion _autorizacion = autorizacion;

    public void Ejecutar(int idReserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaBaja))
        {
            throw new FalloAutorizacionException("No tiene permiso para eliminar reservas.");
        }

        var reserva = _repositorio.ObtenerReservaPorId(idReserva)
            ?? throw new EntidadNotFoundException("Reserva no encontrada.");

        _repositorio.Eliminar(idReserva);
    }
}