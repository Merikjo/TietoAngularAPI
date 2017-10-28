import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title: string = 'Oma Sovellus!';
    orderCount: number = -1;
    customers: string[] = [];
    allCustomers: string[] = [];

    constructor(private http: HttpClient) {
    }

    ngOnInit(): void {
        // Make the HTTP request:
        this.http.get('/api/values/ordercount').subscribe(data => {
            // Read the result field from the JSON response.
            this.orderCount = parseInt(data.toString());
        });

        this.http.get('/api/values/lastnorders/5').subscribe(
            (data: string[]) => {
                this.customers = data;
            });

        this.http.get('/api/values/customers').subscribe(
            (data: string[]) => {
                this.allCustomers = data;
            });
    }
}

