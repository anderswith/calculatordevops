import { Selector } from "testcafe";

fixture("calculator tests").page("http://localhost:4200/");

test("add two numbers", async (t) => {
    await t
        .typeText(Selector("#inputNumberOne"), "5")
        .typeText(Selector("#inputNumberTwo"), "12")
        .click(Selector("#CalculateButton"))
        .expect(Selector("#CalculatorResult").innerText).contains("17");
});

test("is prime", async (t) => {
    await t
        .typeText(Selector("#primeInput"), "2")
        .click(Selector("#primeButton"))
        .expect(Selector("#primeResult").innerText).eql("The number 2 is prime");
});

test("is not prime", async (t) => {
    await t
        .typeText(Selector("#primeInput"), "4")
        .click(Selector("#primeButton"))
        .expect(Selector("#primeResult").innerText).eql("The number 4 is not prime");
});

test("factorial", async (t) => {
    await t
        .typeText(Selector("#factorialInput"), "5")
        .click(Selector("#factorialButton"))
        .expect(Selector("#factorialResult").innerText).eql("The result is: Factorial: 120");
});

/* 
- name: Setup Node.js
      uses: actions/setup-node@v4
      with:
        node-version: 20

      
- name: Install TestCafe
      run: npm install -g testcafe

      
- name: Run TestCafe E2E Tests
      run: testcafe chrome e2e-tests/e2e-tests.js
 */