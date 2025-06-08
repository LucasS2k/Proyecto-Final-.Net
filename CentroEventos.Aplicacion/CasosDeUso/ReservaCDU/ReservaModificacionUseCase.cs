using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaCDU;

public class ModificarReserva
{
    private readonly IServicioAutorizacion _autorizacion;
    private readonly IRepositorioReserva _repositorio;
    private readonly ReservaValidador _validador;

    public ModificarReserva(
        IServicioAutorizacion autorizacion,
        IRepositorioReserva repositorio,
        ReservaValidador validador)
    {
        _autorizacion = autorizacion;
        _repositorio = repositorio;
        _validador = validador;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaModificacion))
        {
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas");
        }
        //REVISAR
        var existente = _repositorio.ObtenerReservaPorId(reserva.Id)
            ?? throw new EntidadNotFoundException("Reserva no encontrada");

        _validador.Validar(reserva);

        _repositorio.Modificar(reserva);
    }
}