<template>
  <div>
    <div class="headerDiv green">
      <h2>Contacts</h2>
      <button v-if="!showAddForm" @click="addContact">Add</button>
      <AddContact
        v-else
        :contractorId="props.contractorId"
        @contactSaved="fetchContacts"
        @cancel="cancelAdd"
      />
    </div>

    <LoaderComp v-if="!contacts"/>
    <ul v-else>
      <li v-for="contact in contacts" :key="contact.contactID">
        <ContactComp 
          :contactId="contact.contactID"
          :contractorId="props.contractorId"
          @contactDeleted="fetchContacts"
          @contactUpdated="fetchContacts"
        />
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue';
import { defineProps, onMounted } from 'vue';
import AddContact from './AddContact.vue';
import { API_BASE_URL } from '../apiConfig';
import LoaderComp from './LoaderComp.vue';
import ContactComp from './ContactComp.vue';

const props = defineProps({
  contractorId: Number
});

const showAddForm = ref(false);
const contacts = ref(null);

const fetchContacts = async () => {
  console.log('fetching contacts...')
  showAddForm.value = false;
  contacts.value = null;
  const response = await fetch(`${API_BASE_URL}/api/contractors/${props.contractorId}/contacts`);
  contacts.value = await response.json();
};

const addContact = () => {
  showAddForm.value = true;
};

const cancelAdd = () => {
  showAddForm.value = false;
};

onMounted(fetchContacts)
watch(() => props.contractorId, (newVal, oldVal) => {
  console.log(`contractorId изменился с ${oldVal} на ${newVal}`);
  fetchContacts();
});
</script>

<style>
.headerDiv {
  display: flex;
}
.headerDiv h2 {
  margin: 0px 15px 0px 0px;
}
</style>