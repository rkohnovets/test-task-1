<template>
  <div class="container">
    <div class="left-pane">
      <div class="headerDiv">
        <h2>Contractors</h2>
        <button v-if="!showAddForm" @click="addContractor">Add</button>
        <AddContractor
          v-else
          @contractorAdded="fetchContractors"
          @cancel="cancelAdd"
        />
      </div>
      <LoaderComp v-if="!contractors"/>
      <ul v-else>
        <li v-for="contractor in contractors" :key="contractor.contractorID">
          <button @click="selectContractor(contractor)">
            {{ contractor.contractorName }}
          </button>
        </li>
      </ul>
    </div>
      <div class="right-pane">
        <contact-list
          v-if="selectedContractorId"
          :contractorId="selectedContractorId"
        />
      </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import ContactList from './ContactList.vue';
import LoaderComp from './LoaderComp.vue';
import { API_BASE_URL } from '../apiConfig';
import AddContractor from './AddContractor.vue';

const contractors = ref(null);
const selectedContractorId = ref(null);
const showAddForm = ref(false);

const selectContractor = (contractor) => {
    console.log(`selected contractor ${contractor.contractorID}`)
    selectedContractorId.value = contractor.contractorID
}

const fetchContractors = async () => {
  contractors.value = null;
  const response = await fetch(`${API_BASE_URL}/api/contractors`);
  contractors.value = await response.json();
};

const addContractor = () => {
  showAddForm.value = true;
}
const cancelAdd = () => {
  showAddForm.value = false;
}

onMounted(fetchContractors);
</script>

<style>
.container {
  display: flex;
}
.left-pane {
  flex: 1;
}
.left-pane h2 {
  margin: 0px 15px 0px 0px;
}
.right-pane {
  flex: 2;
}
.headerDiv {
  display: flex;
}
.headerDiv h2 {
  margin: 0px 15px 0px 0px;
}
</style>