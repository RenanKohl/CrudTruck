describe('Login spec', () => {
  it('should login with success', () => {
    cy.login();
    cy.get('h1 > span').should('have.text', 'Welcome!');
  })


  it('should logout with success', () => {
    cy.login();
    cy.get('#user-dropdown').click();
    cy.get('.nav-item.show > .dropdown-menu > .dropdown-item').click()    
    
    // cy.wait(3000)
    cy.visit('http://localhost:8000')
    cy.get('h1').should('have.text', 'Crud Truck');
  })

  it('should not login with success when fields are empty', () => {
    cy.clearCookies()
    cy.clearLocalStorage()
    cy.visit('http://localhost:8000')
    cy.get(':nth-child(1) > .form-control').click();
    cy.get(':nth-child(2) > .form-control').click();

    cy.get(':nth-child(1) > .text-danger').should('contain', 'is required');
    cy.get(':nth-child(2) > .text-danger').should('contain', 'is required');
  })
})