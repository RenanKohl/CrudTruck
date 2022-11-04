describe('About spec', () => {
  it('Should load system about page', () => {
    // arrange
    cy.selectLanguage()
    cy.login();

    // act
    cy.get('#about').click();

    //assert
    cy.get('#title').should('have.text', 'About');
    cy.get('#breadcrumb-list > .active > span').contains('About');
    cy.get('h1 > span').should('have.text', 'Crud Truck');
  })
})