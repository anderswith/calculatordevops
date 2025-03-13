import { Component } from '@angular/core';
import { CalculatorService } from './calculator.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {log} from '@angular-devkit/build-angular/src/builders/ssr-dev-server';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public isCached: boolean = false;
  public number1: number = 0;
  public number2: number = 0;
  public resultCalculate: number | string = '';
  public resultCheckPrime: string = '';
  public resultFactorial: string = '';
  public history: string[] = [];
  public selectedOperation: string = 'add';
  public primeNumber: number | null = null;
  public factorialNumber: number | null = null;

  constructor(private calculatorService: CalculatorService) {}

  ngOnInit() {
    this.fetchHistory();
  }

  calculate() {
    if (this.number1 !== null && this.number2 !== null) {
      console.log(`Operation: ${this.selectedOperation}, Number1: ${this.number1}, Number2: ${this.number2}`);
      if (this.isCached) {
        this.calculatorService.getCachedResult(this.number1, this.number2, this.selectedOperation)
          .subscribe(
            res => {
              console.log("Cached Result:", res);
              this.resultCalculate = res.result;
            },
            err => {
              console.error("Error fetching cached result:", err);
              this.resultCalculate = 'No cached result found.';
            }
          );
        this.fetchHistory();
      } else {
        this.calculatorService.calculate(this.selectedOperation, this.number1, this.number2)
          .subscribe(
            res => {
              console.log("Calculation Result:", res);
              this.resultCalculate = res;
            },
            err => {
              console.error("Error calculating result:", err);
              this.resultCalculate = 'Error calculating result.';
            }
          );
        this.fetchHistory();
      }
    } else {
      console.error("Number1 or Number2 is null");
    }
  }

  checkPrime() {
    if (this.primeNumber !== null) {
      this.calculatorService.checkPrime(this.primeNumber)
        .subscribe(res => this.resultCheckPrime = res ? `${this.primeNumber} is prime` : `${this.primeNumber} is not prime`);
      this.fetchHistory()
    }
  }

  calculateFactorial() {
    if (this.factorialNumber !== null) {
      this.calculatorService.calculateFactorial(this.factorialNumber)
        .subscribe(res => this.resultFactorial = `Factorial: ${res}`);
      this.fetchHistory();
    }
  }

  fetchHistory() {
    this.calculatorService.getHistory()
      .subscribe(
        res => {
          console.log("History API Response:", res); // Debugging
          this.history = res.map(entry => entry.calcString); // Extract and store only the calcString data
        },
        err => {
          console.error("Error fetching history:", err);
          this.history = ["Error loading history."];
        }
      );
  }
}

