<template>
  <div class="bg-gray-800 p-6 rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-center mb-4 text-white">Список задач</h2>
    <ul>
      <li
        v-for="task in tasks"
        :key="task.id"
        class="p-4 mb-2 bg-gray-700 rounded-lg flex items-center justify-between"
        :class="{ 'bg-gray-600': task.isCompleted }"
      >
        <div class="flex items-center space-x-4">
          <input
            type="checkbox"
            :checked="task.isCompleted"
            @change="toggleCompletion(task)"
            class="w-5 h-5 rounded text-blue-500 focus:ring-blue-500 bg-gray-600 border-gray-500"
          />
          <span
            class="max-w-md text-lg text-white truncate"
            :class="{ 'line-through  text-gray-400': task.isCompleted }"
          >
            {{ task.description }}
          </span>
        </div>
        <button
          @click="deleteTask(task.id)"
          class="bg-red-500 text-white p-2 rounded hover:bg-red-600 transition-colors"
        >
          Удалить
        </button>
      </li>
    </ul>
  </div>
</template>

<script>
import { defineComponent, inject } from 'vue'

export default defineComponent({
  props: {
    tasks: {
      type: Array,
      required: false,
      default: () => [],
    },
  },
  emits: ['task-deleted'],
  setup(props, { emit }) {
    const $http = inject('$http')
    const toggleCompletion = async (task) => {
      task.isCompleted = !task.isCompleted
      await $http.put(`/tasks/complete/${task.id}`, task)
    }

    const deleteTask = async (id) => {
      await $http.delete(`/tasks/${id}`)
      emit('task-deleted', id)
    }

    return {
      toggleCompletion,
      deleteTask,
    }
  },
})
</script>
