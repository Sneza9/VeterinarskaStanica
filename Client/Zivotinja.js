//Cuvanje podataka o zivotinji 
export class Zivotinja{
    constructor(brojKartona, imeZivotinje, imeVlasnika, brojTelefonaVlasnika, vrsta, mesec, veterinarIme, veterinarPrezime, lek)
    {
        this.brojKartona=brojKartona; 
        this.imeZivotinje=imeZivotinje; 
        this.imeVlasnika=imeVlasnika; 
        this.brojTelefonaVlasnika=brojTelefonaVlasnika; 
        this.vrsta = vrsta; 
        this.mesec = mesec; 
        this.veterinarIme=veterinarIme; 
        this.veterinarPrezime=veterinarPrezime; 
        this.lek=lek;
    }

    draw(host){
        //Crta se po jedan red u tabeli 
        var red = document.createElement("tr"); 
        host.appendChild(red);

        var zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.brojKartona;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.imeZivotinje;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.imeVlasnika;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.brojTelefonaVlasnika;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.vrsta;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.mesec;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.veterinarIme;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.veterinarPrezime;
        red.appendChild(zivotinja); 

        zivotinja = document.createElement("td");
        zivotinja.innerHTML=this.lek;
        red.appendChild(zivotinja); 
    }
}