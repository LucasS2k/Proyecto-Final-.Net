using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Interfaces;
public interface IRepositorioPersona
{
    void Agregar(Persona persona);
    void Modificar(Persona persona);
    void Eliminar(int id);
    Persona ObtenerPersonaPorId(int id);
    Persona ObtenerPorDni(string dni);
Persona ObtenerPorEmail(string email);
    List<Persona> Listar();
}