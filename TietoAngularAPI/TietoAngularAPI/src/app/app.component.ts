import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';





@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
    ,
  styles: [`
    .selected {
        background-color: #CFD8DC !important;
        color: white;
    }
        .heroes {
            margin: 0 0 2em 0;
            list-style - type: none;
            padding: 0;
            width: 15em;
        }
        .heroes li {
            cursor: pointer;
            position: relative;
            left: 0;
            background-color: #EEE;
            margin: .5em;
            padding: .3em 0;
            height: 1.6em;
            border-radius: 4px;
        }
        .heroes li.selected:hover {
            background-color: #BBD8DC !important;
            color: white;
        }
        .heroes li:hover {
            color: #607D8B;
            background-color: #DDD;
            left: .1em;
        }
            .heroes.text {
                position: relative;
                top: -3px;
            }
                .heroes.badge {
                    display: inline - block;
                    font-size: small;
                    color: white;
                    padding: 0.8em 0.7em 0 0.7em;
                    background-color: #607D8B;
                    line-height: 1em;
                    position: relative;
                    left: -1px;
                    top: -4px;
                    height: 1.8em;
                    margin-right: .8em;
                    border-radius: 4px 0 0 4px;
                }
  `]

})
export class AppComponent implements OnInit {
    title: string = 'Tietokanta sovellus sivustolle!';
    //orderCount: number = -1;
    projectCount: number = -1;
    personCount: number = -1;
    //customers: string[] = [];
    //acustomers: string[] = [];
    projektit: string[] = [];
    henkilot: string[] = [];

    constructor(private http: HttpClient) {
    }

    ngOnInit(): void {
        // Make the HTTP request:
        //this.http.get('/api/values/ordercount').subscribe(data => {
        //    // Read the result field from the JSON response.
        //    this.orderCount = parseInt(data.toString());
        //});

        this.http.get('/api/values/projectcount').subscribe(data => {
            // Read the result field from the JSON response.
            this.projectCount = parseInt(data.toString());
        });

        this.http.get('/api/values/personcount').subscribe(data => {
            // Read the result field from the JSON response.
            this.personCount = parseInt(data.toString());
        });

        //this.http.get('/api/values/lastnorders/5').subscribe(
        //    (data: string[]) => {
        //        this.customers = data;
        //    });

        //this.http.get('/api/values/allncustomers').subscribe(
        //    (data: string[]) => {
        //        this.acustomers = data;
        //    });

        this.http.get('/api/values/henkilomaara/1000').subscribe(
            (data: string[]) => {
                this.henkilot = data;
            });

        this.http.get('/api/values/projectstatus/1000').subscribe(
            (data: string[]) => {
                this.projektit = data;
            });
    }
}

