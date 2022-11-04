describe('Trucks page spec', () => {
  beforeEach(() =>{
    cy.selectLanguage()
    cy.login();
    cy.get('#trucks').click()
  })

  it('It should load trucks page correctly', () => {
    cy.get('#title').should('have.text', 'Trucks');
    cy.get('#breadcrumb-list > .active > span').contains('Trucks');
  })

  it('It should create FH 2022 truck correctly', () => {
    cy.get('#new-truck').click()
    cy.get('#model').select('FH');
    cy.get('#model-year').select('2022');
    cy.get('#save').click();
  })

  it('It should create FH 2023 truck correctly', () => {
    cy.get('#new-truck').click()
    cy.get('#model').select('FH');
    cy.get('#model-year').select('2023');
    cy.get('#save').click();
  })

  it('It should create FM 2022 truck correctly', () => {
    cy.get('#new-truck').click()
    cy.get('#model').select('FM');
    cy.get('#model-year').select('2022');
    cy.get('#save').click();
  })

  it('It should create FM 2023 truck correctly', () => {
    cy.get('#new-truck').click()
    cy.get('#model').select('FM');
    cy.get('#model-year').select('2023');
    cy.get('#save').click();
  })

  it('It should edit FH 2022 truck correctly', () => {
    cy.get(':nth-child(1) > .cdk-column-actions > #edit').click()
    cy.get('#model-year').select('2023');
    cy.get('#save').click();
    cy.get('#toast-container').contains('Record updated with success')
  })

  it('It should edit FM 2022 truck correctly', () => {
    cy.get(':nth-child(3) > .cdk-column-actions > #edit').click()
    cy.get('#model-year').select('2023');
    cy.get('#save').click();
    cy.get('#toast-container').contains('Record updated with success')
  })

  it('It should delete FH record correctly', () => {
    cy.get('#new-truck').click()
    cy.get('#model').select('FH');
    cy.get('#model-year').select('2022');
    cy.get('#save').click();

    cy.get(':nth-child(5) > .cdk-column-actions > #delete').click()
    cy.get('.swal2-confirm').click();
    cy.get('#toast-container').contains('Record removed with success')
  })

})