using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.SAP;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaCDU;

public class ReservaAltaUseCase(
    IRepositorioReserva repositorio,
    IServicioAutorizacion autorizacion,
    ReservaValidador validador)
{
    private readonly IRepositorioReserva _repositorio = repositorio;
    private readonly IServicioAutorizacion _autorizacion = autorizacion;
    private readonly ReservaValidador _validador = validador;

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta))
        {
            throw new FalloAutorizacionException("No tiene permiso para realizar esto");
        }

        _validador.Validar(reserva);
        _repositorio.Agregar(reserva);
    }
}