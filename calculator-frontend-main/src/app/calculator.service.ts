import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {catchError, Observable, tap, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = 'http://79.76.101.254:5000/api/Calculator'; // Base API URL

  constructor(private http: HttpClient) { }

  calculate(operation: string, a: number, b: number): Observable<number> {
    console.log("Calculating:", operation, a, b);
    const url = `${this.apiUrl}/Calculate${operation}`;
    const body = { a, b };
    console.log("Sending to API:", body);
    return this.http.post<number>(url, body, {
      headers: { 'Content-Type': 'application/json' }
    })
      .pipe(
        tap(response => console.log("API Response:", response))
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

  checkPrime(n: number): Observable<boolean> {
    console.log("Checking prime for:", n);
    const url = `${this.apiUrl}/CalculateIsPrime`;
    const body = { candidate: n };
    console.log("Sending to API:", body);
    return this.http.post<boolean>(url, body, {
      headers: { 'Content-Type': 'application/json' }
    })
      .pipe(
        tap(response => console.log("API Response:", response))
      );
  }

  calculateFactorial(n: number): Observable<number> {
    console.log("Calculating factorial for:", n);
    const url = `${this.apiUrl}/CalculateFactorial`;
    const body = { n };
    console.log("Sending to API:", body);
    return this.http.post<number>(url, body, {
      headers: { 'Content-Type': 'application/json' }
    })
      .pipe(
        tap(response => console.log("API Response:", response))
      );
  }

  getHistory(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Calculations`)
      .pipe(
        tap(response => console.log("History fetched:", response)),
        catchError(error => {
          console.error("Error fetching history:", error);
          return throwError(error);
        })
      );
  }
}

