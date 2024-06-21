<template>
  <div id="app">
    <h1>Contractors and Contacts</h1>
    <div class="container">
      <div class="left-pane">
        <contractor-list :contractors="contractors" @select-contractor="fetchContactsByContractor" />
      </div>
      <div class="right-pane">
        <contact-list 
          :contacts="contacts" 
          :contractors="contractors"
          @contact-deleted="fetchContacts"
          @edit-contact="editContact" 
        />
      </div>
    </div>
  </div>
</template>

<script>
import ContractorList from './components/ContractorList.vue';
import ContactList from './components/ContactList.vue';

export default {
  data() {
    return {
      contractors: [],
      contacts: [],
      selectedContractorId: null,
      editingContact: null
    };
  },
  components: {
    ContractorList,
    ContactList
  },
  methods: {
    async fetchContractors() {
      const response = await fetch('http://localhost:5000/api/contractors');
      this.contractors = await response.json();
    },
    async fetchContacts() {
      const response = await fetch('http://localhost:5000/api/contacts');
      this.contacts = await response.json();
    },
    async fetchContactsByContractor(contractorId) {
      this.selectedContractorId = contractorId;
      const response = await fetch(`http://localhost:5000/api/contractors/${contractorId}/contacts`);
      this.contacts = await response.json();
    },
    editContact(contact) {
      this.editingContact = contact;
      // Здесь вы можете открыть модальное окно для редактирования контакта
    }
  },
  mounted() {
    this.fetchContractors();
    this.fetchContacts();
  }
};
</script>

<style>
.container {
  display: flex;
}
.left-pane {
  flex: 1;
}
.right-pane {
  flex: 2;
}
</style>
