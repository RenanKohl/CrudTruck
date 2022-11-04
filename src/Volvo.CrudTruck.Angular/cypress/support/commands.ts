/// <reference types="cypress" />
// ***********************************************
// This example commands.ts shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
Cypress.Commands.add('login', () => { 
  cy.clearCookies()
  cy.clearLocalStorage()
  cy.visit('http://localhost:8000')
  cy.get(':nth-child(1) > .form-control').type('Rkohl');
  cy.get(':nth-child(2) > .form-control').type('123');
  cy.get('form.ng-dirty > .btn').click();
 })

 Cypress.Commands.add('selectLanguage', () => { 
  cy.clearCookies()
  cy.clearLocalStorage()
  cy.visit('http://localhost:8000')
  cy.get('#current-language').click();
  cy.get('#en-US').click();
 })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })
//
// declare global {
  declare namespace Cypress {
    interface Chainable {
      login(): Chainable<any>
      selectLanguage(): Chainable<any>
      // drag(subject: string, options?: Partial<TypeOptions>): Chainable<Element>
      // dismiss(subject: string, options?: Partial<TypeOptions>): Chainable<Element>
      // visit(originalFn: CommandOriginalFn, url: string, options: Partial<VisitOptions>): Chainable<Element>
    }
  }
// }