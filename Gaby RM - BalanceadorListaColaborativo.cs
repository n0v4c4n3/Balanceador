//Gaby RM - BalanceadorLista (13/09/2015)
using System;
using System.Collections;
namespace BalanceadorLista
{
	public class Persona{
		private string Nombre { get; set; }
		private decimal debe { get; set; }
		private List<Cuenta> cuentas { get; set; }
	}
	public class Deuda{
		private Persona deudor { get; set; }
		private Persona acreedor { get; set; }
		private decimal cantidad { get; set; }
	}
	public class Cuenta{
		private debe { get; set; }
		private List<Persona> grupo { get; set; }
	}
	public class Utilidades{
		//Dadas las personas y deudas que se necesitan balancear
		private void balancear(List<Persona> personas, List<Deuda> deudas){
			//lista (ordenada) de deudores y un stack (LIFO) de acreedores
			private List<Persona> deudores = new List<Persona>(); 
			private Stack<Persona> acreedores = new Stack<Persona>(); 
			//Simplificar las deudas recíprocas
			foreach(Deuda deuda in deudas){
				deuda.acreedor.debe -= deuda.cantidad;
				deuda.deudor.debe += deuda.cantidad;
			}
			//Si la persona termina con "debe" > 0 es deudor, < 0 es acreedor y 0 se ignora
			foreach(Persona persona in personas){
				if (persona.debe > 0){
					deudores.add(persona);
				}
				else if (persona.debe < 0){
					acreedores.push(persona);
				}
			}
			//Se ordenan descendentemente los deudores 
			deudores.OrderByDescending(deudor => deudor.debe);
			//Se ordenan ascendentemente los acreedores, notese que el "debe" de estos son siempre negativos 
			acreedores.OrderBy(acreedor => acreedor.debe); 
			//Recorremos todos los deudores
			foreach(Persona deudor in deudores){
				//Hasta que el deudor deba 0
				while(deudor.debe > 0){
				  Persona acreedor = acreedores.peek(); 
				  decimal cantidad = Math.min(deudor.debe, -acreedor.debe); 
				  deudor.debe -= cantidad;
				  acreedor.debe += cantidad;
			    if(acreedor.debe == 0){ //Si justo da 0 el acreedor fue pagado, ya no me interesa (se podría agregar un offset tipo +-0.50)
			      acreedores.pop() //Elimina del stack
			    }
			    Console.WriteLine(deudor.nombre + " debe a " + acreedor.nombre + " $ " + cantidad); //Para ver como se va calculando
			  }
			}
			foreach(Persona persona in personas){
				if(persona.debe != 0){
					Cuenta cuenta = new Cuenta();
					cuenta.debe = persona.debe;
					cuenta.grupo.Add(personas);
					persona.cuentas.Add(cuenta)
				}
			}
		}
	}
}