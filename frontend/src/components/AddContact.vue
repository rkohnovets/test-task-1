<template>
  <div>
    <h2>Add Contact</h2>
    <form @submit.prevent="addContact">
      <div>
        Contractor id: {{ props.contractorId }}
      </div>
      <div>
        <label for="name">Name:</label>
        <input type="text" v-model="newContact.fullName" required />
      </div>
      <div>
        <label for="email">Email:</label>
        <input type="email" v-model="newContact.email" required />
      </div>
      <button type="submit">Add Contact</button>
    </form>
  </div>
</template>

<script setup>
import { ref, defineProps, defineEmits } from 'vue';
import { API_BASE_URL } from '../apiConfig';

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  contractorId: Number
});

const emit = defineEmits(['contactSaved', 'cancel']);

const newContact = ref({
  fullName: '',
  email: '',
  contractorID: props.contractorId
});

const addContact = async () => {
  try {
    await fetch(`${API_BASE_URL}/api/contacts`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(newContact.value)
    });
    emit('contactSaved');
  } catch (error) {
    console.error('Error adding contact:', error);
  } finally {
    newContact.value = {
      fullName: '',
      email: '',
      contractorID: props.contractorId
    }
    emit('cancel')
  }
};
</script>
