<template>
  <div>
    <form @submit.prevent="addContractor">
      <div>
        <label for="name">Name:</label>
        <input type="text" v-model="newContractor.contractorName" required />
      </div>
      <button type="submit">Add Contractor</button>
    </form>
  </div>
</template>

<script setup>
import { ref, defineEmits } from 'vue';
import { API_BASE_URL } from '../apiConfig';

const newContractor = ref({
  contractorName: ""
})

const emit = defineEmits(['contractorAdded', 'cancel'])

const addContractor = async () => {
  try {
    alert(JSON.stringify(newContractor.value))
    await fetch(`${API_BASE_URL}/api/contractors`, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(newContractor.value)
    })
    emit('contractorAdded')
  }
  catch (e) {
    console.error('Error posting new contractor: ', e)
  }
  finally {
    newContractor.value = {
      contractorName: ""
    };
    emit('cancel');
  }
}

</script>

<style>
</style>