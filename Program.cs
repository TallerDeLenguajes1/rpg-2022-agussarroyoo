// See https://aka.ms/new-console-template for more information

// List<Personaje> EquipoUno = new List<Personaje>();
// List<Personaje> EquipoDos = new List<Personaje>();
// EquipoUno = cargarEquipo(EquipoUno);
// EquipoDos = cargarEquipo(EquipoDos);
// mostrarEquipo(EquipoUno);
// mostrarEquipo(EquipoDos);

Personaje a  = new Personaje();
Personaje b = new Personaje();
mostrarDatos(b);
mostrarCaracteristicas(b);

ataque(a,b);
System.Console.WriteLine("POST ATAQUE");
mostrarDatos(b);
mostrarCaracteristicas(b);

List<Personaje> cargarEquipo(List<Personaje> Lista) {
    for (int i = 0; i < 5; i++)
    {
        Lista.Add(new Personaje());
    }
    return Lista;
}
void mostrarEquipo(List<Personaje> Lista) {
    foreach (Personaje P in Lista)
    {
        System.Console.WriteLine("---DATOS---");
        mostrarDatos(P);
        System.Console.WriteLine("---CARACTERISTICAS---");
        mostrarCaracteristicas(P);
    }
}
void mostrarDatos(Personaje P) {
    System.Console.WriteLine("Mi nombre es " + P.Nombre);
    System.Console.WriteLine("Soy " + P.Tipo);
    System.Console.WriteLine("Me dicen " + P.Apodo);
    System.Console.WriteLine("Nací el " + P.FechaNac.Date.ToShortDateString());
    System.Console.WriteLine("Por lo que tengo " + P.Edad + " años");
    System.Console.WriteLine("Mi salud está al " + P.Salud + "%");
}
void mostrarCaracteristicas(Personaje P) {
    System.Console.WriteLine("Velocidad: " + P.Velocidad + "/10");
    System.Console.WriteLine("Destreza: " + P.Destreza + "/5");
    System.Console.WriteLine("Fuerza: " + P.Fuerza  + "/10");
    System.Console.WriteLine("Nivel : " + P.Nivel + "/10");
    System.Console.WriteLine("Armadura: " + P.Armadura + "/10");
}
 
void combate(List<Personaje> Lista, Personaje L, Personaje V) {
    do
    {
        for (int i = 0; i < 3; i++)
        {
            ataque(L,V);
            ataque(V,L);
        } 
    } while ((L.Salud == V.Salud) && (L.Salud > 0 && V.Salud > 0));
    
    if (L.Salud > V.Salud )
    {
        Lista.Remove(V);
        bonusGanador(L);
    } else {
        Lista.Remove(L);
        bonusGanador(V);
    }
}

void bonusGanador(Personaje P) {
        P.Salud += 25;
        P.Fuerza += new Random().Next(5,10);
        P.Armadura += new Random().Next(1,3);
        P.Destreza += new Random().Next(1,3);
        P.Velocidad += new Random().Next(1,3);
        P.Nivel += 1;
}

void ataque(Personaje L, Personaje V) {
    double PD = L.Destreza * L.Fuerza * L.Nivel;
    double ED = new Random().Next(1,100);
    double VA = PD * ED;
    double PDEF = V.Armadura * V.Velocidad;
    int MDP = 50000;
    double DP = (VA * ED - PDEF) / (MDP) * 100;
    V.Salud = DP;
}
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
        } set {
            if (value > 10)
            {
                velocidad = 10;
            } else {
                velocidad = value;
            }
            
        }
    }
    public int Destreza {
        get{
            return destreza;; 
        }set {
            if (value > 5)
            {
                destreza = 5;
            } else {
                destreza = value;
            }
            
        }
    }
    public int Fuerza {
        get{
            return fuerza; 
        } set {
            if (value > 10)
            {
                fuerza = 10;
            } else {
                fuerza = value;
            }
            
        }
    }
    public int Nivel {
        get{
            return nivel; 
        } set {
            if (value > 10)
            {
                nivel = 10;
            } else{
                nivel = value;
            }
           
        }
    }
    public int Armadura {
        get{
            return armadura; 
        } set {
            if (value > 10)
            {
                armadura = 10;
            } else {
                armadura = value;
            }
            
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
    private double salud;

    public Personaje() {
        this.nombre = nombresPosibles[generarRandom(0,6)];
        this.tipo = tipos[generarRandom(0,3)];
        this.apodo = this.nombre.Substring(0,3);
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
    public double Salud {
        get {
            return salud;
        } set {
            if (value < 0)
            {
                salud = 0;
            } else {
                if (value > 100)
                {
                    salud = 100;
                } else {
                    salud = value; 
                }
               
            }
            
        }
    }
    public int Edad {
        get {
            return edad;
        }
    }
}
 