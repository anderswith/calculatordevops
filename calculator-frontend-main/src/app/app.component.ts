import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'calculator-frontend';
  isCached: boolean = false;
  number1: any;
  number2: any;
  result: string | undefined;
  protected readonly history = history;
  selectedOperation: string | undefined;
  primeNumber: any;
  factorialNumber: any;

  calculate(add: string | undefined) {

  }

  checkPrime() {

  }

  calculateFactorial() {

  }
}
