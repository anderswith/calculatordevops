import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = 'http://localhost:5062'; // Base API URL

  constructor(private http: HttpClient) { }

  calculate(operation: string, a: number, b: number): Observable<number> {
    console.log("Calculating:", operation, a, b);
    const url = `${this.apiUrl}/Calculate${operation}`;
    const params = new HttpParams()
      .set('a', a.toString())
      .set('b', b.toString());
    console.log("Sending to API:", params.toString());
    return this.http.post<number>(url, null, { params })
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
    const params = new HttpParams().set('candidate', n.toString());
    console.log("Sending to API:", params.toString());
    return this.http.post<boolean>(url, null, { params })
      .pipe(
        tap(response => console.log("API Response:", response))
      );
  }

  calculateFactorial(n: number): Observable<number> {
    console.log("Calculating factorial for:", n);
    const url = `${this.apiUrl}/CalculateFactorial`;
    const params = new HttpParams().set('n', n.toString());
    console.log("Sending to API:", params.toString());
    return this.http.post<number>(url, null, { params })
      .pipe(
        tap(response => console.log("API Response:", response))
      );
  }

  getHistory(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Calculations`);
  }
}

