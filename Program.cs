// See https://aka.ms/new-console-template for more information


Personaje a = new Personaje();

System.Console.WriteLine(a.Armadura + " " + a.Destreza + " " + a.Velocidad + " " + a.Fuerza + " " + a.Nivel);



class Caracteristicas {
    
    public int generarRandom(int min, int max) {
        return new Random().Next(min,max);
    }  
    private int velocidad, destreza, fuerza, nivel, armadura;
    public Caracteristicas() {
        this.velocidad = generarRandom(1,10);
        this.destreza = generarRandom(1,5);
        this.fuerza = generarRandom(1,10);
        this.nivel = generarRandom(1,10);
        this.armadura = generarRandom(1,10);
    }

    public int Velocidad {
        get{
            return velocidad; 
        }
    }
    public int Destreza {
        get{
            return destreza;; 
        }
    }
    public int Fuerza {
        get{
            return fuerza; 
        }
    }
    public int Nivel {
        get{
            return nivel; 
        }
    }
    public int Armadura {
        get{
            return armadura; 
        }
    }
    
}
class Personaje:Caracteristicas {
    private string []nombresPosibles = {"Octavia","Bellamy","Clarke","Lexa","Raven","Emori","Indra"};
    private string[]tipos = {"Trikru","Azgeda","Floukru","Sankru"};
    private string nombre;
    private string tipo;
    private string apodo;
    private int dia, mes, anio;
    private DateTime fechaNac;
    private int edad;
    private int salud;

    public Personaje() {
        this.nombre = nombresPosibles[generarRandom(0,6)];
        this.tipo = tipos[generarRandom(0,3)];
        this.apodo = this.nombre.Substring(3);
        this.dia = generarRandom(1,28);
        this.mes = generarRandom(1,12);
        this.anio = generarRandom(1722,2022);
        this.fechaNac = new DateTime(anio,mes,dia);
        this.edad =  DateTime.Today.AddTicks(-this.fechaNac.Ticks).Year - 1;
        this.salud = 100;
    }

    public string Nombre {
        get{
            return nombre;
        } 
    }
    public string Tipo {
        get {
            return tipo;
        }
    }
    public string Apodo {
        get {
            return apodo;
        }
    }
    public DateTime FechaNac {
        get {
            return fechaNac;
        }
    }
    public int Salud {
        get {
            return salud;
        }
    }
    public int Edad {
        get {
            return edad;
        }
    }
}
 