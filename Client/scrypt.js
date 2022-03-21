//Treba napraviti instance veterinara tako sto ih povucemo sa servisa 
import { Veterinar } from "./Veterinar.js";
import { Mesec } from "./Mesec.js";
import { Vrsta } from "./Vrsta.js";
import { VeterinarskaStanica } from "./VeterinarskaStanica.js";

var listaVeterinara = [];
fetch("https://localhost:5001/Veterinar/ProcitajVeterinare")
    .then(p => {
        p.json().then(veterinari => {
            veterinari.forEach(veterinar => {
                var vet = new Veterinar(veterinar.id, veterinar.ime, veterinar.prezime);
                listaVeterinara.push(vet);
            });

            var listaMeseca = [];
            fetch("https://localhost:5001/Pregled/ProcitajMesece")
                .then(p => {
                    p.json().then(meseci => {
                        meseci.forEach(mes => {
                            var m = new Mesec(mes.id, mes.mesec);
                            listaMeseca.push(m);
                        })

                        var listaVrstaZivotinja=[];
                        fetch("https://localhost:5001/VrstaZivotinje/ProcitajVrsteZivotinja")
                            .then(p => {
                                p.json().then(vrste => {
                                    vrste.forEach(vrsta => {
                                        var v = new Vrsta(vrsta.id, vrsta.vrsta);
                                        listaVrstaZivotinja.push(v);
                                    })
                                    var vetStanica = new VeterinarskaStanica(listaVeterinara, listaMeseca, listaVrstaZivotinja); 
                                    vetStanica.draw(document.body); 
                                })
                            })
                            //console.log(listaVrstaZivotinja);
                    })
                })
           
            //console.log(listaMeseca);
            
        })
    })
//console.log(listaVeterinara);




