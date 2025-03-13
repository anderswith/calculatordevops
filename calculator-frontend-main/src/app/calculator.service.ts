import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = 'http://localhost:5062'; // Base API URL

  constructor(private http: HttpClient) { }

  calculate(operation: string, a: number | undefined, b?: number | undefined): Observable<number> {
    console.log("Calculating:", operation, a, b); // Debugging
    let url = `${this.apiUrl}/Calculate${operation}`;
    let body = { a, b };

    console.log("Sending to API:", body); // Debugging

    return this.http.post<number>(url, body).pipe(
      tap(response => console.log("API Response:", response)) // Log the response
    );
  }


  getCachedResult(a: number | undefined, b: number | undefined, operation: string): Observable<any> {
    let params = new HttpParams().set('operation', operation);

    if (a !== undefined) {
      params = params.set('a', a.toString());
    }

    if (b !== undefined) {
      params = params.set('b', b.toString());
    }

    return this.http.get<any>(`${this.apiUrl}/GetCachedResult`, { params });
  }

  checkPrime(n: number | undefined): Observable<boolean> {
    let body = { candidate: n };
    return this.http.post<boolean>(`${this.apiUrl}/CalculateIsPrime`, body);
  }

  calculateFactorial(n: number | undefined): Observable<number> {
    let body = { n };
    return this.http.post<number>(`${this.apiUrl}/CalculateFactorial`, body);
  }

  getHistory(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Calculations`);
  }
}

