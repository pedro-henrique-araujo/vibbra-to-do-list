import api from '../api';

describe('Login and Sign up', () => {
  const disposableUserName = 'disposable';

  async function deleteDisposableUser() {
    await api.delete('User/' + disposableUserName);
  }

  before((done) => {
    deleteDisposableUser().then(() => done());
  });

  after((done) => {
    deleteDisposableUser().then(() => done());
  });

  it('should show invalid form when login user input is empty', () => {
    cy.visit('');
    cy.get('input').focus().blur();
    cy.contains('Este campo é obrigatório').should('be.visible');
    cy.contains('button', 'Entrar').should('be.disabled');
  });

  it('should show validation message when user input does not exist', () => {
    cy.visit('');
    cy.get('input').type(disposableUserName);
    cy.contains('button', 'Entrar').click();
    cy.contains('Usuário inexistente, clique em "Cadastrar-se"').should(
      'be.visible'
    );
    cy.get('#submit').should('be.disabled');
    cy.get('input').type('a');
    cy.get('#submit').should('be.enabled');
  });

  it('should show invalid form when sign up input is empty', () => {
    cy.visit('sign-up');
    cy.get('input').focus().blur();
    cy.contains('Este campo é obrigatório').should('be.visible');
    cy.get('#submit').should('be.disabled');
  });

  it('should sign up', () => {
    cy.visit('');
    cy.contains('button', 'Cadastrar-se').click();
    cy.get('input').type(disposableUserName + '{enter}');
    cy.contains('Sair').should('be.visible');
  });

  it('should show invalid form when sign up already exists', () => {
    cy.visit('sign-up');
    cy.get('input').type(disposableUserName);
    cy.contains('button', 'Cadastrar-se').click();
    cy.contains(
      'Já existe um usuário com este nome, clique em "Entrar"'
    ).should('be.visible');
    cy.get('#submit').should('be.disabled');
    cy.get('input').type('a');
    cy.get('#submit').should('be.enabled');
  });

  it('should login', () => {
    cy.visit('');
    cy.get('input').type(disposableUserName + '{enter}');
    cy.contains('Sair').should('be.visible');
  });

  it('should logout', () => {
    cy.visit('');
    cy.get('input').type(disposableUserName + '{enter}');
    cy.contains('Sair').click();
    cy.contains('Entrar').should('be.visible');
  });
});
