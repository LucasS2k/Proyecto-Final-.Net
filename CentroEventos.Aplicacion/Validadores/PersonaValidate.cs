using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.Validadores;

public class PersonaValidador(IRepositorioPersona repositorio)
{ private readonly IRepositorioPersona _repositorio = repositorio;

    public void Validar(Persona persona)
    {
        //Solo revisa si el campo esta en blanco o si es nulo
        if (string.IsNullOrWhiteSpace(persona.Nombre) ||
            string.IsNullOrWhiteSpace(persona.Apellido) ||
            string.IsNullOrWhiteSpace(persona.DNI) ||
            string.IsNullOrWhiteSpace(persona.Email))
        {
            throw new ValidacionException("Los campos son obligatorios o los datos ingresados son invalidos.");
        }
        //Chequeo de repetidos 
        // 
        // FALTA IMPLEMENTAR
        // FALTA IMPLEMENTAR
        // FALTA IMPLEMENTAR
        // FALTA IMPLEMENTAR
        //
        var existentePorDni = _repositorio.ObtenerPorDni(persona.DNI);
        if (existentePorDni != null && existentePorDni.Id != persona.Id)
        {
            throw new DuplicadoException($"Ya existe una persona con el DNI {persona.DNI}.");
        }

        var existentePorEmail = _repositorio.ObtenerPorEmail(persona.Email);
        if (existentePorEmail != null && existentePorEmail.Id != persona.Id)
        {
            throw new DuplicadoException($"Ya existe una persona con el Email {persona.Email}.");
        }
        
    }
}