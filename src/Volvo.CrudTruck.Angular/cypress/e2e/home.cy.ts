describe('Home page spec', () => {
  it('Should load home page correctly', () => {
    cy.selectLanguage();
    cy.login();

    cy.get('#welcome-message').should('have.text', 'Welcome!')
  })
})