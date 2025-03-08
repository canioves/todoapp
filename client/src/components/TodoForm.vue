<template>
  <div class="bg-gray-800 p-6 rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-center mb-4 text-white">Добавить задачу</h2>
    <form @submit.prevent="addTask">
      <div class="mb-4">
        <input
          v-model="newTask.description"
          class="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500 bg-gray-700 border-gray-600 text-white"
          placeholder="Название задачи"
          required
        />
      </div>
      <div>
        <button
          type="submit"
          class="w-full bg-blue-500 text-white p-2 rounded hover:bg-blue-600 transition-colors"
        >
          Добавить
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import { defineComponent, ref, inject } from 'vue'

export default defineComponent({
  emits: ['task-added'],
  setup(_, { emit }) {
    const $http = inject('$http')
    const newTask = ref({
      description: '',
      isCompleted: false,
    })

    const addTask = async () => {
      const response = await $http.post('/tasks', newTask.value)
      emit('task-added', response.data)
      newTask.value.description = ''
    }

    return {
      newTask,
      addTask,
    }
  },
})
</script>