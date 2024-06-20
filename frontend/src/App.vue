<template>
  <div id="app">
    <ContractorList :contractors="contractors" @contractor-selected="fetchContacts"/>
    <ContactList :contacts="contacts" @save-contact="updateContact"/>
    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
  </div>
</template>

<script>
import ContractorList from './components/ContractorList.vue';
import ContactList from './components/ContactList.vue';

export default {
  name: 'App',
  components: {
    ContractorList,
    ContactList
  },
  data() {
    return {
      contractors: [],
      contacts: [],
      errorMessage: '' // Добавляем поле для ошибки
    }
  },
  mounted() {
    this.fetchContractors();
  },
  methods: {
    fetchContractors() {
      fetch('http://localhost:5000/api/contractors')
        .then(response => {
          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }
          return response.json();
        })
        .then(data => {
          this.contractors = data;
        })
        .catch(error => {
          console.error('Error fetching contractors:', error);
          this.errorMessage = 'Error fetching contractors';
        });
    },
    fetchContacts(contractor) {
      fetch(`http://localhost:5000/api/contractors/${contractor.contractorID}/contacts`)
        .then(response => {
          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }
          return response.json();
        })
        .then(data => {
          this.contacts = data;
        })
        .catch(error => {
          console.error('Error fetching contacts:', error);
          this.errorMessage = 'Error fetching contacts';
        });
    },
    updateContact(contact) {
      console.log(contact)
      fetch(`http://localhost:5000/api/contacts/${contact.contactID}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(contact)
      })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then(updatedContact => {
        const index = this.contacts.findIndex(c => c.contactID === updatedContact.contactID);
        this.$set(this.contacts, index, updatedContact);
      })
      .catch(error => {
        console.error('Error updating contact:', error);
        this.errorMessage = 'Error updating contact';
      });
    }
  }
}
</script>

<style>
#error {
  color: red;
}
#app {
  display: flex;
  justify-content: space-around;
}
</style>
