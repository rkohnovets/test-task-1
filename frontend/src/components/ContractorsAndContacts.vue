<template>
  <loader-comp/>
  <div class="container">
    <div class="left-pane">
      <h2>Contractors</h2>
      <ul>
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

const contractors = ref([]);
const selectedContractorId = ref(null);

const selectContractor = (contractor) => {
    console.log(`selected contractor ${contractor.contractorID}`)
    selectedContractorId.value = contractor.contractorID
}

const fetchContractors = async () => {
  const response = await fetch(`${API_BASE_URL}/api/contractors`);
  contractors.value = await response.json();
};

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
</style>