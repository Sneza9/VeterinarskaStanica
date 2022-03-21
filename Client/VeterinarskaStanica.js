import { Zivotinja } from "./Zivotinja.js";
export class VeterinarskaStanica {
    constructor(listaVeterinara, listaMeseca, listaVrstaZivotinja) {
        this.listaVeterinara = listaVeterinara;
        this.listaMeseca = listaMeseca;
        this.listaVrstaZivotinja = listaVrstaZivotinja;
        //Pamtimo kontejner po kome cemo da crtamo 
        this.container = null;
        this.mesec = null;
        this.vrste = null;
    }

    //#region Crtanje stranice

    //Ovoj metodi prosledjujemo host 
    //Ovde cemo da napravimo inicijalni prikaz forme 
    draw(host) {
        this.container = document.createElement("div");
        this.container.className = "MainContainer";
        //Taj kontejner appendujemo na host 
        host.appendChild(this.container);
        //Forma se sastoji iz 2 dela, za pretragu i za prikaz 
        //I stavljamo ih na glavni kontejner 

        let containerForm = document.createElement("div");
        containerForm.className = "Form";
        this.container.appendChild(containerForm);

        let containerTable = document.createElement("div");
        containerTable.className = "Table";
        this.container.appendChild(containerTable);

        this.drawForm(containerForm);
        this.drawTable(containerTable);
    }
    drawTable(host) {
        var table = document.createElement("table");
        table.className = "tabela";
        host.appendChild(table);

        //Zaglavlje tabele
        var thead = document.createElement("thead");
        table.appendChild(thead);

        var red = document.createElement("tr");
        thead.appendChild(red);

        //Body tabele
        var tbody = document.createElement("tbody");
        tbody.className = "TableBody";
        table.appendChild(tbody);

        //Header tabele
        var th;
        var zaglavlje = ["Broj kartona", "Ime životinje", "Ime vlasnika", "Broj telefona", "Vrsta životinje", "Mesec pregleda", "Ime veterinara", "Prezime veterinara", "Lek"];
        zaglavlje.forEach(element => {
            //Za svaki element iz zaglavlja pravi se th 
            th = document.createElement("th");
            th.innerHTML = element;
            red.appendChild(th);
        });

    }
    drawForm(host) {

        //Crta red za formu
        let red = this.drawRow(host);
        let labPretraga = document.createElement("label");
        labPretraga.innerHTML = "Pretraga ";
        labPretraga.className = "lab";
        red.appendChild(labPretraga);

        //Veterinari

        red = this.drawRow(host);
        let labVeterinari = document.createElement("label");
        labVeterinari.innerHTML = "Veterinar ";
        labVeterinari.className = "lab";
        red.appendChild(labVeterinari);

        red = this.drawRow(host);
        let combo = document.createElement("select");
        red.appendChild(combo);

        //Opcije select dela 
        let option;

        this.listaVeterinara.forEach(veterinar => {
            option = document.createElement("option");
            //Ime i prezime veterinara  
            option.innerHTML = veterinar.ime + " " + veterinar.prezime;
            //Saljemo id bazi (value vrednost) 
            option.value = veterinar.id;
            combo.appendChild(option);
        });

        //Meseci

        red = this.drawRow(host); 
        let labMeseci = document.createElement("label");
        labMeseci.innerHTML = "Mesec ";
        labMeseci.className = "lab";
        red.appendChild(labMeseci);

        //Za svaki mesec se prave opcije i za njih se pravi kontejner 
        this.mesec = document.createElement("div");
        this.mesec.className = "checkBox";
        red.appendChild(this.mesec);
        //2 diva i dodajemo ih na checkBox 
        let checkBoxPrvi = document.createElement("div");
        checkBoxPrvi.className = "checkBoxPrvi";
        this.mesec.appendChild(checkBoxPrvi);
        let checkBoxDrugi = document.createElement("div");
        checkBoxDrugi.className = "checkBoxDrugi";
        this.mesec.appendChild(checkBoxDrugi);

        red = this.drawRow(host);
        let labVrste = document.createElement("label");
        labVrste.innerHTML = "Vrste ";
        labVrste.className = "lab";
        red.appendChild(labVrste);
        this.vrste = document.createElement("div");
        this.vrste.className = "checkBoxVrste";
        //Prvi div 
        red.appendChild(this.vrste);
        let checkBoxPrviVrste = document.createElement("div");
        checkBoxPrviVrste.className = "checkBoxPrviVrste";
        this.vrste.appendChild(checkBoxPrviVrste);
        //Drugi div 
        let checkBoxDrugiVrste = document.createElement("div");
        checkBoxDrugiVrste.className = "checkBoxDrugiVrste";
        this.vrste.appendChild(checkBoxDrugiVrste);


        //Treba konkretno svaki 
        //Meseci se rasporedjuju u prvi i drugi div po id-ju 
        let checkBoxes;
        let checkBoxDiv;
        let labCheck;
        this.listaMeseca.forEach((mesec, index) => {

            checkBoxDiv = document.createElement("div"); 
            checkBoxes = document.createElement("input");
            checkBoxes.type = "checkbox";
            checkBoxes.value = mesec.id;
            checkBoxDiv.appendChild(checkBoxes); 

            labCheck = document.createElement("label");
            labCheck.innerHTML = mesec.mesec;
            checkBoxDiv.appendChild(labCheck);
            if (index < 6) {
                checkBoxPrvi.appendChild(checkBoxDiv);
            }
            else {
                checkBoxDrugi.appendChild(checkBoxDiv);
            }
        })

        //Vrste 

        let checkBoxesVrste;
        let labCheckVrste
        let checkBoxDivVrsta;
        this.listaVrstaZivotinja.forEach((vrsta, index) => {
            checkBoxDivVrsta = document.createElement("div");
            checkBoxesVrste = document.createElement("input");
            checkBoxesVrste.type = "checkbox";
            checkBoxesVrste.value = vrsta.id;
            checkBoxDivVrsta.appendChild(checkBoxesVrste);

            labCheckVrste = document.createElement("label");
            labCheckVrste.innerHTML = vrsta.vrsta;
            checkBoxDivVrsta.appendChild(labCheckVrste);
            if (index % 2 == 0) {
                checkBoxPrviVrste.appendChild(checkBoxDivVrsta);
            }
            else {
                checkBoxDrugiVrste.appendChild(checkBoxDivVrsta);
            }
        })

        //Pretraga

        red = this.drawRow(host);
        let btnPretrazi = document.createElement("button");
        btnPretrazi.onclick = (ev) => this.pretraziZivotinje();
        btnPretrazi.innerHTML = "Pretraži ";
        red.appendChild(btnPretrazi);

        //Dodavanje novog leka 

        red = this.drawRow(host);
        let labPregled = document.createElement("label");
        labPregled.innerHTML = "Unesite novi pregled ";
        labPregled.className = "lab";
        red.appendChild(labPregled);

        red = this.drawRow(host);
        let brojKartona = document.createElement("label");
        brojKartona.innerHTML = "Broj kartona ";
        brojKartona.className = "lab";
        red.appendChild(brojKartona);

        red = this.drawRow(host);
        var tbBrojKartona = document.createElement("input");
        tbBrojKartona.type = "number";
        tbBrojKartona.className = "brojKartona";
        red.appendChild(tbBrojKartona);

        red = this.drawRow(host);
        let lek = document.createElement("label");
        lek.innerHTML = "Lek ";
        lek.className = "lab";
        red.appendChild(lek);

        red = this.drawRow(host);
        var tbLek = document.createElement("input");
        tbLek.type = "text";
        tbLek.className = "lek";
        red.appendChild(tbLek);

        red = this.drawRow(host);
        let btnUnesi = document.createElement("button");
        btnUnesi.onclick = (ev) => this.dodajLek(tbBrojKartona.value, tbLek.value);
        btnUnesi.innerHTML = "Dodaj lek";
        red.appendChild(btnUnesi);

        //Unos nove zivotinje, azuriranje i brisanje postojece zivotinje 

        red = this.drawRow(host);
        let labUnesite = document.createElement("label");
        labUnesite.innerHTML = "Unesite novu životinju ";
        labUnesite.className = "lab";
        red.appendChild(labUnesite);

        red = this.drawRow(host);
        let labBrojKartona = document.createElement("label");
        labBrojKartona.innerHTML = "Broj kartona ";
        labBrojKartona.className = "lab";
        red.appendChild(labBrojKartona);

        red = this.drawRow(host);
        var tbBrojKartonaNova = document.createElement("input");
        tbBrojKartonaNova.type = "number";
        tbBrojKartonaNova.className = "brKartonaNova";
        red.appendChild(tbBrojKartonaNova);

        red = this.drawRow(host);
        let labImeZivotinje = document.createElement("label");
        labImeZivotinje.innerHTML = "Ime životinje ";
        labImeZivotinje.className = "lab";
        red.appendChild(labImeZivotinje);

        red = this.drawRow(host);
        var tbImeZivotinje = document.createElement("input");
        tbImeZivotinje.type = "text";
        tbImeZivotinje.className = "imeZivotinje";
        red.appendChild(tbImeZivotinje);

        red = this.drawRow(host);
        let labImeVlasnika = document.createElement("label");
        labImeVlasnika.innerHTML = "Ime vlasnika ";
        labImeVlasnika.className = "lab";
        red.appendChild(labImeVlasnika);

        red = this.drawRow(host);
        var tbImeVlasnika = document.createElement("input");
        tbImeVlasnika.type = "text";
        tbImeVlasnika.className = "imeVlasnika";
        red.appendChild(tbImeVlasnika);

        red = this.drawRow(host);
        let labBrTel = document.createElement("label");
        labBrTel.innerHTML = "Broj telefona ";
        labBrTel.className = "lab";
        red.appendChild(labBrTel);

        red = this.drawRow(host);
        var tbBrojTelefona = document.createElement("input");
        tbBrojTelefona.type = "text";
        tbBrojTelefona.className = "brojTelefona";
        red.appendChild(tbBrojTelefona);

        red = this.drawRow(host);
        let btnUnesiNovuZivotinju = document.createElement("button");
        btnUnesiNovuZivotinju.onclick = (ev) => this.unesiNovuZivotinju(tbBrojKartonaNova.value, tbImeZivotinje.value, tbImeVlasnika.value, tbBrojTelefona.value);
        btnUnesiNovuZivotinju.innerHTML = "Dodaj životinju ";
        red.appendChild(btnUnesiNovuZivotinju);

        let btnAzurirajZivotinju = document.createElement("button");
        btnAzurirajZivotinju.onclick = (ev) => this.azurirajZivotinju(tbBrojKartonaNova.value, tbImeZivotinje.value, tbImeVlasnika.value, tbBrojTelefona.value);
        btnAzurirajZivotinju.innerHTML = "Ažuriraj podatke";
        red.appendChild(btnAzurirajZivotinju);

        let btnObrisiZivotinju = document.createElement("button");
        btnObrisiZivotinju.onclick = (ev) => this.obrisiZivotinju(tbBrojKartonaNova.value);
        btnObrisiZivotinju.innerHTML = "Obriši životinju ";
        red.appendChild(btnObrisiZivotinju);

    }
    drawRow(host) {
        let red = document.createElement("div");
        red.className = "red";
        host.appendChild(red);
        return red;
    }
    //#endregion

    //#region Rad sa servisom i bazom 
    pretraziZivotinje() {
        let veterinar = this.container.querySelector("option:checked").value; 
        //console.log("Veterinar");
        //console.log(veterinar);

        let meseci = this.mesec.querySelectorAll("input[type='checkbox']:checked");
        //console.log(meseci);

        //Validacija: Ako korisnik ne cekira nista 
        if (meseci.length == 0) {
            alert("Izaberite odgovarajuce mesece!");
            return;
        }

        //Konkateniraju se cekirani meseci sa "m" i to se salje servisu kroz url 
        let nizMeseca = "";
        for (let i = 0; i < meseci.length; i++) {
            nizMeseca = nizMeseca.concat(meseci[i].value, "m");
        }
        //console.log("niz meseca");
        //console.log(nizMeseca); 

        let vrsta = this.vrste.querySelectorAll("input[type='checkbox']:checked");
        //console.log(vrsta);

        //Validacija
        if (vrsta.length != 1) {
            alert("Mozete odabrati jednu vrstu zivotinje!");
            return;
        }

        //Zivotinje se prikazuju u tabeli na stranici 
        this.prikaziZivotinje(veterinar, nizMeseca, vrsta[0].value);        
    }
    prikaziZivotinje(veterinarID, nizMeseca, vrstaID) {
        //Fetch koji vraca sve zivotinje sa selektovanim veterinarom, mesecima i vrstom 
        //Logika za pribavljanje podataka iz baze podataka 
        fetch("https://localhost:5001/VetStanica/ProcitajZivotinje/" + veterinarID + "/" + nizMeseca + "/" + vrstaID,
            {
                method: "GET"
            }).then(z => {
                if (z.ok) {
                    var telobody = this.obrisiRedoveTabele();
                    z.json().then(zivotinje => {
                        //Crta se telo tabele 
                        zivotinje.forEach(zivotinja => {
                            var ziv = new Zivotinja(zivotinja.brojKartona, zivotinja.imeZivotinje, zivotinja.imeVlasnika, zivotinja.brojTelefonaVlasnika, zivotinja.vrsta, zivotinja.mesec, zivotinja.veterinarIme, zivotinja.veterinarPrezime, zivotinja.lek);
                            ziv.draw(telobody);
                        })
                    })
                }
            })
    }
    dodajLek(brojKartona, lek) {
        //Validacija 
        if (brojKartona === "") {
            alert("Unesite broj kartona!");
            return;
        }
        if (lek === "") {
            alert("Unesite naziv leka!");
            return;
        }

        //Vraca sve elemente koji su tipa type checkbox i koji su cekirani 
        let meseci = this.mesec.querySelectorAll("input[type='checkbox']:checked");
        //Validacija: Ako korisnik nije nista cekirao ili je cekirao vise meseca 
        if (meseci === null || meseci.length != 1) {
            alert("Morate selektovati jedan mesec!");
            return;
        }
        
        //Broj kartona, lek, izabrani mesec je meseci[0].value 
        let veterinar = this.container.querySelector("select");
        var veterinarID = veterinar.options[veterinar.selectedIndex].value;
        //console.log("Broj kartona " + brojKartona);
        //console.log("Lek " + lek);
        //console.log("Mesec " + meseci[0].value);
        //console.log("Veterinar " + veterinarID);
        

        //Fetch, logika za dodavanje leka u bazu 
        fetch("https://localhost:5001/Pregled/DodajLek/" + brojKartona + "/" + veterinarID + "/" + meseci[0].value + "/" + lek,
            {
                method: "POST"
            }).then(z => {
                if (z.ok) {
                    //Prikaz svih pregleda te zivotinje 
                    var telobody = this.obrisiRedoveTabele();
                    z.json().then(zivotinje => {
                        zivotinje.forEach(zivotinja => {
                            var ziv = new Zivotinja(zivotinja.brojKartona, zivotinja.imeZivotinje, zivotinja.imeVlasnika, zivotinja.brojTelefonaVlasnika, zivotinja.vrsta, zivotinja.mesec, zivotinja.veterinarIme, zivotinja.veterinarPrezime, zivotinja.lek);
                            //console.log(ziv);
                            ziv.draw(telobody);
                        })
                    })
                }
            })
    }
    unesiNovuZivotinju(brojKartonaNova, imeZivotinje, imeVlasnika, brojTelefona) {
        //Validacija 
        if (brojKartonaNova === "") {
            alert("Unesite broj kartona!");
            return;
        }
        if (imeZivotinje === "") {
            alert("Unesite ime zivotinje!");
            return;
        }
        if (imeVlasnika === "") {
            alert("Unesite ime vlasnika!");
            return;
        }
        if (brojTelefona === "") {
            alert("Unesite broj telefona vlasnika!");
            return;
        }

        let vrste = this.vrste.querySelectorAll("input[type='checkbox']:checked");
        //Validacija: Ako korisnik nije nista cekirao ili je cekirao vise meseca 
        if (vrste === null || vrste.length != 1) {
            alert("Selektujte jednu vrstu zivotinje!");
            return;
        }

        //console.log("Broj kartona " + brojKartonaNova);
        //console.log("ime zivotinje " + imeZivotinje);
        //console.log("ime vlasnika " + imeVlasnika);
        //console.log("broj telefona vlasnika " + brojTelefona);
        //console.log("vrsta id " + vrste[0].value);

        //Fetch, logika za dodavanje nove zivotinje u bazu 
        fetch("https://localhost:5001/VetStanica/DodajZivotinju/" + brojKartonaNova + "/" + imeZivotinje + "/" + imeVlasnika + "/" + brojTelefona + "/" + vrste[0].value,
            {
                method: "POST"
            }).then(z => {
                if (z.ok) {
                    alert("Zivotinja je uspesno dodata!");
                }
            })

    }
    azurirajZivotinju(brojKartonaNova, imeZivotinje, imeVlasnika, brojTelefona) {
        //Validacija 
        if (brojKartonaNova === "") {
            alert("Unesite broj kartona!");
            return;
        }
        if (imeZivotinje === "") {
            alert("Unesite ime zivotinje!");
            return;
        }
        if (imeVlasnika === "") {
            alert("Unesite ime vlasnika!");
            return;
        }
        if (brojTelefona === "") {
            alert("Unesite broj telefona vlasnika!");
            return;
        }

        fetch("https://localhost:5001/VetStanica/AzurirajZivotinju/" + brojKartonaNova + "/" + imeZivotinje + "/" + imeVlasnika + "/" + brojTelefona,
            {
                method: "PUT"
            }).then(z => {
                if (z.ok) {
                    alert("Podaci su uspesno azurirani!");
                }
            })
    }
    obrisiZivotinju(brojKartona) {
        //Validacija 
        if (brojKartona === "") {
            alert("Unesite broj kartona kako biste obrisali zivotinju!");
            return;
        }
        fetch("https://localhost:5001/VetStanica/ObrisiZivotinju/" + brojKartona,
            {
                method: "DELETE"
            })
            .then(z => {
                if (z.ok) {
                    alert("Zivotinja je uspesno obrisana!");
                }
            })
    }
    
    //#endregion

    //#region Helperi
    obrisiRedoveTabele() {
        var tbody = document.querySelector(".TableBody");
        var roditelj = tbody.parentNode;
        roditelj.removeChild(tbody);

        tbody = document.createElement("tbody");
        tbody.className = "TableBody";
        roditelj.appendChild(tbody);
        return tbody;
    }
    //#endregion

}